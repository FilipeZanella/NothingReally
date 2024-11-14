using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected StateContext context { get; private set; }

    public State(StateContext _context)
    {
        context = _context;
    }

    public abstract void OnEnterState();
    public abstract void OnExitState();

    protected void ChangeState(State newState)
    {
        context.SetState(newState);
    }
}
