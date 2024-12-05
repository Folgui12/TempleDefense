using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyStunedState<T> : State<T>
{
    ISteering _steering;
    BaseEnemyModel _model;

    float timer;
    float StunedTime;

    public EnemyStunedState(BaseEnemyModel model, float stunedTime)
    {
        _model = model;
        StunedTime = stunedTime;
    }

    public override void Enter()
    {
        base.Enter();

        timer = 0;

        //Comenzar animación de stun y efectos quizá, así como tambien algún sonido?
    }


    public override void Execute()
    {
        base.Execute();

        _model.Move(Vector3.zero);
        //_model.LookDir();

        timer += Time.deltaTime;

        if(timer >= StunedTime)
        {
            _model.Stuned = false;
        }

    }

    public override void Sleep()
    {
        timer = 0;
    }
}

