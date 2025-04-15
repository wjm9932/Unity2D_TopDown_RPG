using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    public IState currentState { get; private set; }
    private IState pendingState;

    public void ChangeState(IState newState)
    {
        pendingState = newState;
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void LateUpdate()
    {
        currentState?.LateUpdate();

        ApplyPendingStateChange();
    }

    private void ApplyPendingStateChange()
    {
        if (pendingState != null)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = pendingState;
            currentState.Enter();
            pendingState = null;
        }
    }
}