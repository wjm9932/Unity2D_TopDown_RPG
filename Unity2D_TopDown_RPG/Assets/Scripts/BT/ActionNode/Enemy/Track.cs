using UnityEngine;

public class Track : IAction
{
    private Blackboard blackboard;
    private MeleeEnemy owner;
    private float stopDistance;
    public Track(Blackboard blackBoard, float stopDistance)
    {
        this.blackboard = blackBoard;
        owner = blackboard.GetData<MeleeEnemy>("owner");
        this.stopDistance = stopDistance;
    }

    public void OnEnter()
    {
        Debug.Log("Enter Track");

        owner.animationHandler.animator.SetBool(owner.animationHandler.animationData.animParameterData.trackParameterHash, true);
    }
    public NodeState Execute()
    {
        UpdateAnimations();
        if(Vector2.Distance(blackboard.GetData<Vector3>("spotPosition"), owner.transform.position) < stopDistance)
        {
            return NodeState.Success;
        }

        return NodeState.Running;
    }

    public void ExecuteInFixedUpdate()
    {
        GetInterestWeight();
        Move();
    }
    public void OnExit()
    {
        owner.animationHandler.animator.SetBool(owner.animationHandler.animationData.animParameterData.trackParameterHash, false);
    }

    void GetInterestWeight()
    {
        Vector2 toTarget = (blackboard.GetData<Vector3>("spotPosition") - owner.transform.position).normalized;

        for (int i = 0; i < owner.steeringAgent.directions.Length; i++)
        {
            float dot = Vector2.Dot(toTarget, owner.steeringAgent.directions[i].normalized);
            owner.steeringAgent.interest[i] = 1 + dot;
        }
    }

    private int GetFinalDirection()
    {
        int desiredDirIndex = 0;

        for (int i = 0; i < owner.steeringAgent.directions.Length; i++)
        {
            owner.steeringAgent.final[i] = owner.steeringAgent.interest[i] * (1f - owner.steeringAgent.danger[i]);
            if(owner.steeringAgent.final[i] > owner.steeringAgent.final[desiredDirIndex])
            {
                desiredDirIndex = i;
            }
        }

        return desiredDirIndex;
    }

    private void Move()
    {
        int dir = GetFinalDirection();

        Vector2 targetSpeed = owner.steeringAgent.directions[dir] * owner.movementSO.runMaxSpeed;

        Vector2 speedDif = targetSpeed - owner.rb.linearVelocity;
        Vector2 movement = speedDif * owner.movementSO.runAccelAmount;

        owner.rb.AddForce(movement, ForceMode2D.Force);
    }
    private void UpdateAnimations()
    {
        int closestDirIndex = Utility.GetClosestDirectionIndex(Utility.GetAnimationDirections(), owner.rb.linearVelocity);

        owner.animationHandler.animator.SetFloat(owner.animationHandler.animationData.animParameterData.horizontalParameterHash, Utility.GetAnimationDirections()[closestDirIndex].x);
        owner.animationHandler.animator.SetFloat(owner.animationHandler.animationData.animParameterData.verticalParameterHash, Utility.GetAnimationDirections()[closestDirIndex].y);
    }
}
