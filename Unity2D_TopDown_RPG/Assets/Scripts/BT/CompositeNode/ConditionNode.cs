using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : INode
{
    private Func<bool> condition;
    private ICondition conditionNode;
    public ConditionNode(Func<bool> condition)
    {
        this.condition = condition;
    }
    public ConditionNode(ICondition conditionNode)
    {
        this.conditionNode = conditionNode;
    }
    public NodeState Evaluate()
    {
        if (conditionNode == null)
        {
            return condition() ? NodeState.Success : NodeState.Failure;
        }
        else
        {
            return conditionNode.IsSatisFy() ? NodeState.Success : NodeState.Failure;
        }
    }
}
