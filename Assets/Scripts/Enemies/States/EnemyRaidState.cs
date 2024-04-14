using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaidState<T> : State<T>
{
    ISteering _steering;
    BaseEnemyModel _model;
    Transform _currentBiulding;
    ObstacleAvoidance _obs;
    public EnemyRaidState(BaseEnemyModel model, Transform target, ObstacleAvoidance obs, ISteering steering)
    {
        _model = model;
        _currentBiulding = target;
        _obs = obs;
        _steering = steering;
    }
    public override void Execute()
    {
        base.Execute();

        //A: Enemy
        //B: Target

        //(b-a).n
        GameObject currnetBiulding = _model.CheckClosest();
        var dir = _obs.GetDir(_steering.GetDir());
        _model.Move(dir.normalized);
        _model.LookDir(dir.normalized);
    }
}
