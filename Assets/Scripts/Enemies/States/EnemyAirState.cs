using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class EnemyAirState<T> : State<T>
{
    BaseEnemyModel _model;

    public EnemyAirState(BaseEnemyModel model)
    {
        _model = model;
    }
    public override void Enter()
    {
        base.Enter();
        _model.RagdollOn();
    }
    public override void Execute()
    {
        base.Execute();
    }
}
