using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    private StateContext context;

    public State(StateContext _context)
    {
        context = _context;
    }

    public abstract void OnEnterState();
    public abstract void OnExitState();

    protected void ChangeState(State newState)
    {

    }
}
