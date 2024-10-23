using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseEnemyController : ManagedUpdateBehavior
{
    BaseEnemyModel _model;
    BaseEnemyView _view;
    public AudioSource audioSource;
    public int enemyType;

    #region STEERING
    public Rigidbody target;
    public float timePrediction;
    public float angle;
    public float radius;
    public LayerMask maskObs;
    public float personalArea;
    ObstacleAvoidance _obstacleAvoidance;
    #endregion

    #region INTERFACE
    [SerializeField] LoS _los;
    FSM<StatesEnum> _fsm;
    ITreeNode _root;
    ISteering _steering;
    #endregion

    EnemyRaidState<StatesEnum> _stateFollowPoints;
    EnemyToTowerState<StatesEnum> _toTowerState;
    private void Awake()
    {
        _model = GetComponent<BaseEnemyModel>();
        _view = GetComponent<BaseEnemyView>();
        _los = GetComponent<LoS>();
    }

    protected override void Start()
    {
        base.Start();
        InitializeSteerings();
        InitializedTree();
        InitializeFSM();       
    }
    private void OnEnable()
    {
        InitializedTree();
        InitializeFSM();
    }
    void InitializeFSM()
    {
        var idle = new EnemyIdleState<StatesEnum>();
        var dead = new EnemyDeathState<StatesEnum>(_model, _view);
        var attack = new EnemyAttackState<StatesEnum>(_model, _view);
        _stateFollowPoints = new EnemyRaidState<StatesEnum>(_model, audioSource, enemyType);
        var air = new EnemyAirState<StatesEnum>(_model);
        _toTowerState = new EnemyToTowerState<StatesEnum>(_model, _model._currentBuilding, _steering, _obstacleAvoidance);


        idle.AddTransition(StatesEnum.Dead, dead);
        idle.AddTransition(StatesEnum.Attack, attack);
        idle.AddTransition(StatesEnum.Raid, _stateFollowPoints);
        idle.AddTransition(StatesEnum.InAir, air);
        idle.AddTransition(StatesEnum.ToTower, _toTowerState);

        attack.AddTransition(StatesEnum.Idle, idle);
        attack.AddTransition(StatesEnum.Dead, dead);
        attack.AddTransition(StatesEnum.Raid, _stateFollowPoints);
        attack.AddTransition(StatesEnum.InAir, air);
        attack.AddTransition(StatesEnum.ToTower, _toTowerState);

        _stateFollowPoints.AddTransition(StatesEnum.Idle, idle);
        _stateFollowPoints.AddTransition(StatesEnum.Dead, dead);
        _stateFollowPoints.AddTransition(StatesEnum.Attack, attack);
        _stateFollowPoints.AddTransition(StatesEnum.InAir, air);
        _stateFollowPoints.AddTransition(StatesEnum.ToTower, _toTowerState);

        air.AddTransition(StatesEnum.Idle, idle);
        air.AddTransition(StatesEnum.Dead, dead);
        air.AddTransition(StatesEnum.Attack, attack);
        air.AddTransition(StatesEnum.Raid, _stateFollowPoints);
        air.AddTransition(StatesEnum.ToTower, _toTowerState);

        _toTowerState.AddTransition(StatesEnum.Idle, idle);
        _toTowerState.AddTransition(StatesEnum.Dead, dead);
        _toTowerState.AddTransition(StatesEnum.Attack, attack);
        _toTowerState.AddTransition(StatesEnum.Raid, _stateFollowPoints);
        _toTowerState.AddTransition(StatesEnum.InAir, air);


        _fsm = new FSM<StatesEnum>(idle);
    }
    void InitializeSteerings()
    {
        _steering = GetComponent<FlockingManager>();
        _obstacleAvoidance = new ObstacleAvoidance(_model.transform, angle, radius, personalArea, maskObs);
    }
    void InitializedTree()
    {
        //Actions
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var dead = new ActionNode(() => _fsm.Transition(StatesEnum.Dead));
        var attack = new ActionNode(() => _fsm.Transition(StatesEnum.Attack));
        var raid = new ActionNode(() => _fsm.Transition(StatesEnum.Raid));
        var air = new ActionNode(() => _fsm.Transition(StatesEnum.InAir));
        var toTower = new ActionNode(() => _fsm.Transition(StatesEnum.ToTower));

        //Question
        var qAttackRange = new QuestionNode(QuestionAttackRange, attack, toTower);
        var qLoS = new QuestionNode(QuestionLoS, qAttackRange, raid);
        var qInAir = new QuestionNode(() => !_model.OnHand && _model.OnGround, qLoS, air);
        var qHasLife = new QuestionNode(() => _model.CurrentLife > 0, qInAir, dead);

        _root = qHasLife;
    }
    bool QuestionAttackRange()
    {
        return _los.CheckRange(_model._currentBuilding.transform, _model._stats.attackRange);
    }
    bool QuestionLoS()
    {
        return _los.CheckRange(_model._currentBuilding.transform, _model._stats.viewRange);
    }
    protected override void CustomLightUpdate()
    {
        base.CustomLightUpdate();
        _model._currentBuilding = _model.CheckClosest();
        _fsm.OnUpdate();
        _root.Execute();
    }
    
    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_los.Origin, radius);
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Arrow"))
        {
            BulletMovement arrowHit = other.gameObject.GetComponent<BulletMovement>();

            _model.TakeDamage(arrowHit.Damage);
        }
    }

    public IPoints GetRaidStateWaypoints => _stateFollowPoints;

}
