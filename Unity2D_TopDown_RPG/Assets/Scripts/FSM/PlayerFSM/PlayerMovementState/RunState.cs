using UnityEngine;

public class RunState : IState
{
    private PlayerStateMachine sm;
    private Animator animator;
    private PlayerAnimationData animData;

    public RunState(PlayerStateMachine sm)
    {
        this.sm = sm;
        animator = sm.owner.animHandler.animator;
        animData = sm.owner.animationData;
    }

    public void Enter()
    {
        animator.SetTrigger(animData.runParameterHash);
    }
    public void Update()
    {
        if (sm.owner.input.moveInput.sqrMagnitude < 0.01f)
        {
            sm.ChangeState(sm.idleState);
        }
    }
    public void FixedUpdate()
    {
        if (sm.currentState != this)
        {
            return;
        }

        Run();
    }
    public void LateUpdate()
    {
        if(sm.currentState != this)
        {
            return;
        }

        animator.SetFloat(animData.horizontalParameterHash, sm.owner.input.moveInput.x);
        animator.SetFloat(animData.verticalParameterHash, sm.owner.input.moveInput.y);
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

    private void Run()
    {
        Vector2 targetSpeed = sm.owner.input.moveInput * sm.owner.movementType.runMaxSpeed;

        Vector2 speedDif = targetSpeed - sm.owner.rb.linearVelocity;
        Vector2 movement = speedDif * sm.owner.movementType.runAccelAmount;

        sm.owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
