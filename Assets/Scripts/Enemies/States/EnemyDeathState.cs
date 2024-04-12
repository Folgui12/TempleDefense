using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState<T> : State<T>
{
    //Crear variable del modelo base de enemigo, así como tambien su constructor.
    BaseEnemyModel _model;

    public EnemyDeathState(BaseEnemyModel model)
    {
        _model = model;
    }

    public override void Enter()
    {
        base.Enter();
        _model.Dead();
        // Llamar al Dead dentro de la variable del modelo base.
    }
}
