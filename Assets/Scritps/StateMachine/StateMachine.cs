using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Tick(Time.deltaTime);
            //alternative writing is : currentState?.Tick(Time.deltaTime);
        }

    }
}