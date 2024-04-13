using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseEnemyController : MonoBehaviour
{
    BaseEnemyModel _model;
    public GameObject _currentBiulding;
    public float attackRange;

    #region STEERING
    public Rigidbody target;
    public float timePrediction;
    public float angle;
    public float radius;
    public LayerMask maskObs;
    ObstacleAvoidance _obstacleAvoidance;
    #endregion

    #region INTERFACE
    LoS _los;
    FSM<StatesEnum> _fsm;
    ITreeNode _root;
    ISteering _steering;
    #endregion

    private void Awake()
    {
        _model = GetComponent<BaseEnemyModel>();
        _los = GetComponent<LoS>();
    }

    private void Start()
    {
        InitializeSteerings();
        InitializeFSM();
        InitializedTree();
    }

    //private void Update()
    //{
    //    if (_model.CheckDistance())
    //        _model.Attack();
    //    else
    //    {
    //        _model.CheckClosest();
    //        if (_model.CheckYPosition())
    //            _model.GoToMainBuilding();
    //        Debug.Log("Raid");
    //    }
    //}

    void InitializeFSM()
    {
        var idle = new EnemyIdleState<StatesEnum>();
        var dead = new EnemyDeathState<StatesEnum>(_model);
        var attack = new EnemyAttackState<StatesEnum>(_model);
        var raid = new EnemyRaidState<StatesEnum>(_model, _currentBiulding.transform);
        var air = new EnemyAirState<StatesEnum>(_model);
        var steering = new EnemySteeringState<StatesEnum>(_model, _steering, _obstacleAvoidance);


        idle.AddTransition(StatesEnum.Dead, dead);
        idle.AddTransition(StatesEnum.Attack, attack);
        idle.AddTransition(StatesEnum.Raid, raid);
        idle.AddTransition(StatesEnum.InAir, air);
        idle.AddTransition(StatesEnum.Steer, steering);

        dead.AddTransition(StatesEnum.Idle, idle);
        dead.AddTransition(StatesEnum.Attack, attack);
        dead.AddTransition(StatesEnum.Raid, raid);

        attack.AddTransition(StatesEnum.Idle, idle);
        attack.AddTransition(StatesEnum.Dead, dead);
        attack.AddTransition(StatesEnum.Raid, raid);
        attack.AddTransition(StatesEnum.InAir, air);

        raid.AddTransition(StatesEnum.Idle, idle);
        raid.AddTransition(StatesEnum.Dead, dead);
        raid.AddTransition(StatesEnum.Attack, attack);
        raid.AddTransition(StatesEnum.InAir, air);
        raid.AddTransition(StatesEnum.Steer, steering);

        air.AddTransition(StatesEnum.Idle, idle);
        air.AddTransition(StatesEnum.Dead, dead);
        air.AddTransition(StatesEnum.Attack, attack);
        air.AddTransition(StatesEnum.Raid, raid);

        steering.AddTransition(StatesEnum.Idle, idle);

        _fsm = new FSM<StatesEnum>(steering);
    }
    void InitializeSteerings()
    {
        var seek = new Seek(_model.transform, _currentBiulding.transform);
        var flee = new Flee(_model.transform, _currentBiulding.transform);
        var pursuit = new Pursuit(_model.transform, _currentBiulding.GetComponent<Rigidbody>(), timePrediction);
        var evade = new Evade(_model.transform, _currentBiulding.GetComponent<Rigidbody>(), timePrediction);
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
        var air = new ActionNode(() => _fsm.Transition(StatesEnum.InAir));

        //Question
        var qAttackRange = new QuestionNode(QuestionAttackRange, attack, idle);
        var qLoS = new QuestionNode(QuestionLoS, qAttackRange, raid);
        var qInAir = new QuestionNode(() => _model.transform.position.y < 3, qLoS, air);
        var qHasLife = new QuestionNode(() => _model.Life > 0, qInAir, dead);

        _root = qHasLife;
    }
    bool QuestionAttackRange()
    {
        return _los.CheckRange(_currentBiulding.transform, attackRange);
    }
    bool QuestionLoS()
    {
        return _los.CheckRange(_currentBiulding.transform);
    }
    private void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();
    }

}
