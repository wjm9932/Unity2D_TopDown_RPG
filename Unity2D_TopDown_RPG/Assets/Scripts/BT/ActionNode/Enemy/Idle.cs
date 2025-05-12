using UnityEngine;

public class Idle : IAction
{
    private Blackboard blackboard;
    private MeleeEnemy owner;
    public Idle(Blackboard blackBoard)
    {
        this.blackboard = blackBoard;
        owner = blackboard.GetData<MeleeEnemy>("owner");
    }

    public void OnEnter()
    {
        Debug.Log("Enter Idle");

        UpdateAnimations();
    }
    public NodeState Execute()
    {
        return NodeState.Running;
    }

    public void ExecuteInFixedUpdate()
    {
        Move();
    }
    public void OnExit()
    {
    }

    private void Move()
    {
        Vector2 speedDif = Vector2.zero - owner.rb.linearVelocity;
        Vector2 movement = speedDif * owner.movementSO.runDeccelAmount;

        owner.rb.AddForce(movement, ForceMode2D.Force);
    }

    private void UpdateAnimations()
    {
        int closestDirIndex = Utility.GetClosestDirectionIndex(Utility.GetAnimationDirections(), owner.forward);

        owner.animationHandler.animator.SetFloat(owner.animationHandler.animationData.animParameterData.horizontalParameterHash, Utility.GetAnimationDirections()[closestDirIndex].x);
        owner.animationHandler.animator.SetFloat(owner.animationHandler.animationData.animParameterData.verticalParameterHash, Utility.GetAnimationDirections()[closestDirIndex].y);
    }
}
