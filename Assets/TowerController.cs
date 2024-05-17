using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private TowerModel _model;
    private LoS _los;
    FSM<StatesEnum> _fsm;
    ITreeNode _root;

    private void Awake()
    {
        _los = GetComponent<LoS>();
        _model = GetComponent<TowerModel>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializedTree();
        InitializeFSM();
    }
    
    void InitializeFSM()
    {
        var idle = new DefenseIdleState<StatesEnum>(_model);
        var dead = new DefenseDeadState<StatesEnum>(_model);
        var attack = new DefenseAttackState<StatesEnum>(_model);

        idle.AddTransition(StatesEnum.Dead, dead);
        idle.AddTransition(StatesEnum.Attack, attack);

        attack.AddTransition(StatesEnum.Idle, idle);
        attack.AddTransition(StatesEnum.Dead, dead);

        _fsm = new FSM<StatesEnum>(idle);
    }
    
    void InitializedTree()
    {
        //Actions
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var dead = new ActionNode(() => _fsm.Transition(StatesEnum.Dead));
        var attack = new ActionNode(() => _fsm.Transition(StatesEnum.Attack));

        //Question
        var qEnemyInRange = new QuestionNode(() => _model.CheckClosestEnemy()!=null, attack, idle);
        var qHasLife = new QuestionNode(() => _model.CurrentLife > 0, qEnemyInRange, dead);

        _root = qHasLife;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _model.CurrentLife -= 1;
        }
    }

    private void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();
    }
}
