using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    public void Enter();
    public void Execute();
    public void Sleep();
    void AddTransitions(T input, IState<T> state);
    void RemoveTransition(T input);
    void RemoveTransition(IState<T> state);
    IState<T> GetTransition(T input);
}
