using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState<T> : State<T>
{
    BaseEnemyModel _model;
    BaseEnemyView _view;

    public EnemyAttackState(BaseEnemyModel model, BaseEnemyView view)
    {
        _model = model;
        _view = view;
    }

    public override void Enter()
    {
        base.Enter();
        _view.Attacking = true;
        _view.CanIdle();
    }

    public override void Execute()
    {
        base.Execute();
        _model.Move(Vector3.zero);
        _view.StartAttackAnimation();
    }

    public override void Sleep()
    {
        base.Sleep();
        _view.Attacking = false;
        _view.StopIdle();
    }
}
