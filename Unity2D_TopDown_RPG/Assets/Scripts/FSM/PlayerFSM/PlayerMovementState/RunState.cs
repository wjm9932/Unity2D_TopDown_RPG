using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class RunState : IState
{
    private PlayerStateMachine sm;
    private AnimationHandler<PlayerAnimationData> animationHandler;

    public RunState(PlayerStateMachine sm)
    {
        this.sm = sm;
        animationHandler = sm.owner.animationHandler;
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
        if(sm.owner.input.dodgeBufferTime > 0f)
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

    private void Run()
    {
        Vector2 targetSpeed = sm.owner.input.moveInput * sm.owner.movementType.runMaxSpeed;

        Vector2 speedDif = targetSpeed - sm.owner.rb.linearVelocity;
        Vector2 movement = speedDif * sm.owner.movementType.runAccelAmount;

        sm.owner.rb.AddForce(movement, ForceMode2D.Force);
    }

    private void SetAnimationValue()
    {
       
    }
}
