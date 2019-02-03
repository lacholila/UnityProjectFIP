using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    private IState currentState;
    private IState previousState;
    
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        previousState = currentState;
        currentState = newState;
        currentState.Enter();
    }

    public void ExecuteStateUpdate()
    {
        var runningState = currentState;
        if (runningState != null)
        {
            currentState.Execute();
        }
    }

    public void SwitchToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }
}