using UnityEngine;

public class IdleState : IState
{
    private PlayerStateMachine sm;
    private Animator animator;
    private PlayerAnimationData animData;

    public IdleState(PlayerStateMachine sm)
    {
        this.sm = sm;
        animator = sm.owner.animHandler.animator;
        animData = sm.owner.animationData;
    }

    public void Enter()
    {
        float currentHorizontal = animator.GetFloat(animData.horizontalParameterHash);
        float currentVertical = animator.GetFloat(animData.verticalParameterHash);

        animator.SetTrigger(animData.idleParameterHash);

        animator.SetFloat(animData.horizontalParameterHash, currentHorizontal);
        animator.SetFloat(animData.verticalParameterHash, currentVertical);
    }
    public void Update()
    {
        if(sm.owner.input.moveInput.sqrMagnitude > 0.01f)
        {
            sm.ChangeState(sm.runState);
        }
    }
    public void FixedUpdate()
    {
        if(sm.currentState != this)
        {
            return;
        }

        Deaccel();
    }
    public void LateUpdate()
    {
    }
    public void Exit()
    {
    }
    public void OnAnimationEnterEvent()
    {
    }
    public void OnAnimationExitEvent()
    {
    }
    public void OnAnimationTransitionEvent()
    {
    }
    public void OnAnimatorIK()
    {
    }

    private void Deaccel()
    {
        Vector2 speedDif = Vector2.zero - sm.owner.rb.linearVelocity;
        Vector2 movement = speedDif * sm.owner.movementType.runDeccelAmount;

        sm.owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
