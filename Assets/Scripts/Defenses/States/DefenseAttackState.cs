using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseAttackState<T> : State<T>
{
    private TowerModel _tModel;
    
    // Start is called before the first frame update
    public DefenseAttackState(TowerModel model)
    {
        _tModel = model;
    }

    public override void Execute()
    {
        base.Execute();
        _tModel.CheckClosestEnemy();
        ArcherEventManager.Execute();
    }
}
