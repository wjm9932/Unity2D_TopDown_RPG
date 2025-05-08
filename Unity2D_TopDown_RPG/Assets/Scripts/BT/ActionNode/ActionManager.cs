using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    void OnEnter();
    void OnExit();
    NodeState Execute();

    void ExecuteInFixedUpdate();
}

public class ActionManager
{
    private IAction currentAction;
    private NodeState currentState;
    public void ChangeAction(IAction newAction)
    {
        if (currentAction != newAction || currentState == NodeState.Success)
        {
            currentAction?.OnExit();
            currentAction = newAction;
            currentAction.OnEnter();
        }
    }

    public NodeState ExecuteCurrentAction()
    {
        if (currentAction == null)
        {
            currentState = NodeState.Failure;
            return currentState;
        }
        else
        {
            currentState = currentAction.Execute();
            return currentState;
        }
    }

    public void ExecuteCurrentActionInFixedUpdate()
    {
        if(currentState == NodeState.Running)
        {
            currentAction?.ExecuteInFixedUpdate();
        }
    }
}
