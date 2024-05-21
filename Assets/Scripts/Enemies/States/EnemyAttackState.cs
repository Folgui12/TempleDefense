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
    public override void Execute()
    {
        base.Execute();
        _model.Move(Vector3.zero);
        _view.StartAttackAnimation();
        //EnemyEventManager.Execute();
    }
}
