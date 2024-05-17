using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseIdleState<T> : State<T>
{
    private TowerModel _model; 
    
    // Start is called before the first frame update
    public DefenseIdleState(TowerModel model)
    {
        _model = model;
    }

    public override void Execute()
    {
        base.Execute();
        //Debug.Log("Idling");
    }
}
