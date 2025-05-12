using UnityEngine;

public class Attack : IAction
{
    private Blackboard blackboard;
    private MeleeEnemy owner;
    public Attack(Blackboard blackBoard)
    {
        this.blackboard = blackBoard;
        owner = blackboard.GetData<MeleeEnemy>("owner");
    }

    public void OnEnter()
    {
        owner.animationHandler.animator.SetBool(owner.animationHandler.animationData.animParameterData.attackParameterHash, true);
        Debug.Log("Enter Attack");
    }

    public NodeState Execute()
    {
        AnimatorStateInfo stateInfo = owner.animationHandler.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= owner.animationHandler.animationData.animEventTimeData.attackFinishTime && stateInfo.IsName(owner.animationHandler.animationData.animParameterData.attackAnimationName))
        {
            owner.ResetAttackTime();
            return NodeState.Success;
        }

        return NodeState.Running;
    }

    public void ExecuteInFixedUpdate()
    {
        Move();
    }

    public void OnExit()
    {
        owner.animationHandler.animator.SetBool(owner.animationHandler.animationData.animParameterData.attackParameterHash, false);
    }

    private void Move()
    {
        Vector2 targetSpeed = Vector2.zero * owner.movementSO.fleeMaxSpeed;
        Vector2 speedDif = targetSpeed - owner.rb.linearVelocity;
        Vector2 movement = speedDif * owner.movementSO.runDeccelAmount;

        owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
