using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseStartingState<T> : State<T>
{
    private TowerModel _tModel;

    public DefenseStartingState(TowerModel model)
    {
        _tModel = model;
    }
    public override void Execute()
    {
        base.Execute();
        _tModel.DelayAnim();
        if(_tModel.delayTime >= 0.8f)
        {
            _tModel.finishDelay = true;
        }
    }
}
