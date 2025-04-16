using UnityEngine;

public class Attack_2State : IState
{
    private AnimationHandler<PlayerAnimationData> animationHandler;
    private PlayerStateMachine sm;

    public Attack_2State(PlayerStateMachine sm)
    {
        this.sm = sm;
        animationHandler = sm.owner.animationHandler;
    }

    public void Enter()
    {
        animationHandler.animator.SetBool(animationHandler.animationData.animParameterData.attack_2ParameterHash, true);
        if (sm.owner.input.moveInput.sqrMagnitude > 0.0001f)
        {
            animationHandler.animator.SetFloat(animationHandler.animationData.animParameterData.horizontalParameterHash, sm.owner.input.moveInput.x);
            animationHandler.animator.SetFloat(animationHandler.animationData.animParameterData.verticalParameterHash, sm.owner.input.moveInput.y);
        }
    }

    public void FixedUpdate()
    {
        Deaccel();
    }
    public void Update()
    {
        if (animationHandler.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationHandler.animationData.animEventTimeData.attack_2FinishTime)
        {
            sm.ChangeState(sm.runState);
        }
    }
    public void LateUpdate()
    {
    }
    public void Exit()
    {
        animationHandler.animator.SetBool(animationHandler.animationData.animParameterData.attack_2ParameterHash, false);
    }
    private void Deaccel()
    {
        Vector2 speedDif = Vector2.zero - sm.owner.rb.linearVelocity;
        Vector2 movement = speedDif * sm.owner.movementType.runDeccelAmount;

        sm.owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
