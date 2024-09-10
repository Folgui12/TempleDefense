using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseAttackState<T> : State<T>
{
    private TowerModel _tModel;
    private ArcherModel[] Archers;
    
    // Start is called before the first frame update
    public DefenseAttackState(TowerModel model)
    {
        _tModel = model;
    }

    public override void Enter()
    {
        base.Enter();
        Archers = _tModel.GetComponentsInChildren<ArcherModel>();
    }

    public override void Execute()
    {
        base.Execute();

        if (_tModel._currentEnemy == null)
            _tModel.CheckClosestEnemy(); 
    
        foreach(ArcherModel archer in Archers)
        {
            archer.StartShootAnimation();
        }

    }
}
