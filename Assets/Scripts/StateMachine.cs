using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    private IState currentState;
    private IState previousState;

    public void ChangeState(IState newState) {
        if (currentState != null) {
            this.currentState.Exit();
        }

        this.previousState = this.currentState;
        this.currentState = newState;
        this.currentState.Enter();
    }

    public void ExecuteStateUpdate() {
        var runningState = this.currentState;
        if (runningState != null) {
            this.currentState.Execute();
        }
    }

    public void SwitchToPreviousState() {
        this.currentState.Exit();
        this.currentState = this.previousState;
        this.currentState.Enter();
    }
}
