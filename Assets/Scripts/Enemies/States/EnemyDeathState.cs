using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState<T> : State<T>
{
    //Crear variable del modelo base de enemigo, así como tambien su constructor.
    BaseEnemyModel _model;
    BaseEnemyView _view;

    public EnemyDeathState(BaseEnemyModel model, BaseEnemyView view)
    {
        _model = model;
        _view = view; 
    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("muerte");
        _model.Dead();
        _view.StartAttackAnimation();
        // Llamar al Dead dentro de la variable del modelo base.
    }
}
