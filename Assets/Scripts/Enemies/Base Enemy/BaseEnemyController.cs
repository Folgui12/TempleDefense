using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    BaseEnemyModel _model;
    public Transform _currentBiulding;
    public float attackRange;
    LoS _los;
    FSM<StatesEnum> _fsm;
    ITreeNode _root;

    private void Awake()
    {
        _model = GetComponent<BaseEnemyModel>();
        _los = GetComponent<LoS>();
    }

    private void Start()
    {
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
        var raid = new EnemyRaidState<StatesEnum>(_model, _currentBiulding);
        var air = new EnemyAirState<StatesEnum>(_model);


        idle.AddTransition(StatesEnum.Dead, dead);
        idle.AddTransition(StatesEnum.Attack, attack);
        idle.AddTransition(StatesEnum.Raid, raid);
        idle.AddTransition(StatesEnum.InAir, air);

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

        air.AddTransition(StatesEnum.Idle, idle);
        air.AddTransition(StatesEnum.Dead, dead);
        air.AddTransition(StatesEnum.Attack, attack);
        air.AddTransition(StatesEnum.Raid, raid);

        _fsm = new FSM<StatesEnum>(raid);
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
        return _los.CheckRange(_currentBiulding, attackRange);
    }
    bool QuestionLoS()
    {
        return _los.CheckRange(_currentBiulding);
    }
    private void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();
    }

}
