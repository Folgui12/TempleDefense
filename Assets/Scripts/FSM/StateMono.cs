using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMono<T> : MonoBehaviour, IState<T>
{
    private Dictionary<T, IState<T>> _transitions;
    
    public virtual void Enter()
    {
        
    }

    public virtual void Execute()
    {
        
    }

    public virtual void Sleep()
    {
        
    }

    public void AddTransitions(T input, IState<T> state)
    {
        _transitions[input] = state;
    }

    public void RemoveTransition(T input)
    {
        if(_transitions.ContainsKey(input))
            _transitions.Remove(input);
    }
    
    public void RemoveTransition(IState<T> state)
    {
        foreach (var item in _transitions)
        {
            T key = item.Key;
            IState<T> value = item.Value;
            if (value == state)
            {
                _transitions.Remove(key);
                break;
            }
        }
    }

    public IState<T> GetTransition(T input)
    {
        if (_transitions.ContainsKey(input))
            return _transitions[input];
        else
            return null;

    }
}
