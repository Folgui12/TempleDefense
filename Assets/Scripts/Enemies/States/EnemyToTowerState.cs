using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyToTowerState<T> : State<T>
{
    ISteering _steering;
    BaseEnemyModel _model;
    ObstacleAvoidance _obs;
    GameObject _target;
    public EnemyToTowerState(BaseEnemyModel model, GameObject target, ISteering steering, ObstacleAvoidance obs)
    {
        _steering = steering;
        _model = model;
        _obs = obs;
        _target = target;
    }
    public override void Execute()
    {
        _model._leaderBehaviour.target = _model.CheckClosest();
        var dir = _obs.GetDir(_steering.GetDir(), false);
        _model.Move(_model.transform.forward);
        _model.LookDir(dir);
    }
}
