using UnityEngine;

public class RunState : IState
{
    private PlayerStateMachine sm;
    private AnimHandler animationHandler;

    public RunState(PlayerStateMachine sm)
    {
        this.sm = sm;
        animationHandler = sm.owner.animHandler;
    }

    public void Enter()
    {
        animationHandler.animator.SetBool(animationHandler.animationData.runParameterHash, true);
    }
    public void FixedUpdate()
    {
        Run();
    }
    public void Update()
    {
        if (sm.owner.input.moveInput.sqrMagnitude < 0.01f)
        {
            sm.ChangeState(sm.idleState);
        }
        if(sm.owner.input.isDodge == true)
        {
            sm.ChangeState(sm.dodgeState);
        }
    }
    public void LateUpdate()
    {
        animationHandler.animator.SetFloat(animationHandler.animationData.horizontalParameterHash, sm.owner.lookDir.x);
        animationHandler.animator.SetFloat(animationHandler.animationData.verticalParameterHash, sm.owner.lookDir.y);
    }
    public void Exit()
    {
        animationHandler.animator.SetBool(animationHandler.animationData.runParameterHash, false);
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
