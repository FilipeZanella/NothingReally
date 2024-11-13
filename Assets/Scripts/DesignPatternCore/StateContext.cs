using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateContext 
{
    private State currentState;

    public void SetState(State newState) 
    {
        if (currentState != newState) 
        {
            if (currentState != null)
            {
                currentState.OnExitState();
            }
            currentState = newState;
            currentState.OnEnterState();
        }
    }
}
