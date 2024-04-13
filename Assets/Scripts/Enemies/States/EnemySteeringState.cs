using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySteeringState<T> : State<T>
{
    ISteering _steering;
    BaseEnemyModel _model;
    ObstacleAvoidance _obs;
    public EnemySteeringState(BaseEnemyModel model, ISteering steering, ObstacleAvoidance obs)
    {
        _steering = steering;
        _model = model;
        _obs = obs;
    }
    public override void Execute()
    {
        var dir = _obs.GetDir(_steering.GetDir(), true);
        _model.Move(dir);
        _model.LookDir(dir);
    }
}
