using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAirState<T> : State<T>
{
    BaseEnemyModel _model;

    public EnemyAirState(BaseEnemyModel model)
    {
        _model = model;
    }
    public override void Execute()
    {
        base.Execute();
        _model.Move(Vector3.zero);
    }
}
