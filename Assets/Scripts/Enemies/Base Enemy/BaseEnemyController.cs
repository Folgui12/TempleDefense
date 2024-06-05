using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseEnemyController : MonoBehaviour
{
    BaseEnemyModel _model;
    BaseEnemyView _view;

    #region STEERING
    public Rigidbody target;
    public float timePrediction;
    public float angle;
    public float radius;
    public LayerMask maskObs;
    ObstacleAvoidance _obstacleAvoidance;
    #endregion

    #region INTERFACE
    [SerializeField] LoS _los;
    FSM<StatesEnum> _fsm;
    ITreeNode _root;
    ISteering _steering;
    #endregion

    EnemyFollowPointsState<StatesEnum> _stateFollowPoints;
    private void Awake()
    {
        _model = GetComponent<BaseEnemyModel>();
        _view = GetComponent<BaseEnemyView>();
        _los = GetComponent<LoS>();
    }

    private void Start()
    {
        InitializeSteerings();
        InitializedTree();
        InitializeFSM();
    }

    void InitializeFSM()
    {
        var idle = new EnemyIdleState<StatesEnum>();
        var dead = new EnemyDeathState<StatesEnum>(_model, _view);
        var attack = new EnemyAttackState<StatesEnum>(_model, _view);
        var raid = new EnemyRaidState<StatesEnum>(_model, _model._currentBuilding.transform, _obstacleAvoidance, _steering);
        var air = new EnemyAirState<StatesEnum>(_model);
        _stateFollowPoints = new EnemyFollowPointsState<StatesEnum>(_model);


        idle.AddTransition(StatesEnum.Dead, dead);
        idle.AddTransition(StatesEnum.Attack, attack);
        //idle.AddTransition(StatesEnum.Raid, raid);
        idle.AddTransition(StatesEnum.InAir, air);
        idle.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);

        dead.AddTransition(StatesEnum.Idle, idle);
        dead.AddTransition(StatesEnum.Attack, attack);
        //dead.AddTransition(StatesEnum.Raid, raid);

        attack.AddTransition(StatesEnum.Idle, idle);
        attack.AddTransition(StatesEnum.Dead, dead);
        //attack.AddTransition(StatesEnum.Raid, raid);
        attack.AddTransition(StatesEnum.InAir, air);
        attack.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);


        //raid.AddTransition(StatesEnum.Idle, idle);
        //raid.AddTransition(StatesEnum.Dead, dead);
        //raid.AddTransition(StatesEnum.Attack, attack);
        //raid.AddTransition(StatesEnum.InAir, air);

        _stateFollowPoints.AddTransition(StatesEnum.Idle, idle);
        _stateFollowPoints.AddTransition(StatesEnum.Attack, attack);
        _stateFollowPoints.AddTransition(StatesEnum.InAir, air);
        _stateFollowPoints.AddTransition(StatesEnum.Dead, dead);

        air.AddTransition(StatesEnum.Idle, idle);
        air.AddTransition(StatesEnum.Dead, dead);
        air.AddTransition(StatesEnum.Attack, attack);
        /*air.AddTransition(StatesEnum.Raid, raid)*/;

        _fsm = new FSM<StatesEnum>(_stateFollowPoints);
    }
    void InitializeSteerings()
    {
        var seek = new Seek(_model,_model.transform, _model._currentBuilding.transform);

        _steering = seek;
        _obstacleAvoidance = new ObstacleAvoidance(_model.transform, angle, radius, maskObs);
    }
    void InitializedTree()
    {
        //Actions
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var dead = new ActionNode(() => _fsm.Transition(StatesEnum.Dead));
        var attack = new ActionNode(() => _fsm.Transition(StatesEnum.Attack));
        var raid = new ActionNode(() => _fsm.Transition(StatesEnum.Raid));
        var follow = new ActionNode(() => _fsm.Transition(StatesEnum.Waypoints));
        var air = new ActionNode(() => _fsm.Transition(StatesEnum.InAir));

        //Question
        var qAttackRange = new QuestionNode(QuestionAttackRange, attack, follow);
        var qFollowPoints = new QuestionNode(() => _stateFollowPoints.IsFinishPath, qAttackRange, follow);
        var qLoS = new QuestionNode(QuestionLoS, qAttackRange, qFollowPoints);
        var qInAir = new QuestionNode(() => !_model.OnHand && _model.OnGround, qLoS, air);
        var qHasLife = new QuestionNode(() => _model.CurrentLife > 0, qInAir, dead);

        _root = qFollowPoints;
    }
    bool QuestionAttackRange()
    {
        return _los.CheckRange(_model._currentBuilding.transform, _model._stats.attackRange);
    }
    bool QuestionLoS()
    {
        return _los.CheckRange(_model._currentBuilding.transform, _model._stats.viewRange);
    }
    private void Update()
    {
        _model._currentBuilding = _model.CheckClosest();
        _fsm.OnUpdate();
        _root.Execute();
    }

    public IPoints GetStateWaypoints => _stateFollowPoints;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_los.Origin, radius);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Arrow"))
        {
            BulletMovement arrowHit = other.gameObject.GetComponent<BulletMovement>();

            _model.TakeDamage(arrowHit.Damage);
        }
    }

}
