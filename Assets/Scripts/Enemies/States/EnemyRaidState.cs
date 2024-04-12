using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaidState<T> : State<T>
{
    BaseEnemyModel _model;
    Transform _currentBiulding;

    public EnemyRaidState(BaseEnemyModel model, Transform target)
    {
        _model = model;
        _currentBiulding = target;
    }
    public override void Execute()
    {
        base.Execute();

        //A: FakeCrash
        //B: Target

        //(b-a).n
        GameObject currnetBiulding = _model.CheckClosest();
        Vector3 dir = currnetBiulding.transform.position - _model.transform.position;
        _model.Move(dir.normalized);
        _model.LookDir(dir.normalized);
    }
}
