using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    private IState<T> _current;

    public void SetInit(IState<T> initState)
    {
        _current = initState;
        _current.Enter();
    }

    public void OnUpdate()
    {
        if(_current != null)
            _current.Execute();
    }

    public void MakeTransition(T input)
    {
        IState<T> newState = _current.GetTransition(input);
        if (newState != null)
        {
            _current.Sleep();
            _current = newState;
            _current.Enter();
        }
    }

    public IState<T> CurrentState => _current;
}
