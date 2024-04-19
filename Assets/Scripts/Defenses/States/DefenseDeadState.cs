using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseDeadState<T> : State<T>
{
    private TowerModel _model; 
    
    // Start is called before the first frame update
    public DefenseDeadState(TowerModel model)
    {
        _model = model;
    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("Defense Die");
    }
}
