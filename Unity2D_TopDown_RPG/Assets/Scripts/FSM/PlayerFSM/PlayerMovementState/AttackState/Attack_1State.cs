using UnityEngine;

public class Attack_1State : IState
{
    private AnimationHandler<PlayerAnimationData> animationHandler;
    private PlayerStateMachine sm;

    public Attack_1State(PlayerStateMachine sm)
    {
        this.sm = sm;
        animationHandler = sm.owner.animationHandler;
    }

    public void Enter()
    {
        animationHandler.animator.SetBool(animationHandler.animationData.animParameterData.attack_1ParameterHash, true);
    }

    public void FixedUpdate()
    {
        Deaccel();
    }
    public void Update()
    {
        float normalizedTime = animationHandler.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (normalizedTime >= animationHandler.animationData.animEventTimeData.attack_2InputEnableTime && sm.owner.input.autoAttackBufferTime >= 0)
        {
            sm.ChangeState(sm.attack_2State);
        }
        if (normalizedTime >= animationHandler.animationData.animEventTimeData.attack_1FinishTime)
        {
            sm.ChangeState(sm.runState);
        }
    }
    public void LateUpdate()
    {
    }
    public void Exit()
    {
        animationHandler.animator.SetBool(animationHandler.animationData.animParameterData.attack_1ParameterHash, false);
    }
    private void Deaccel()
    {
        Vector2 speedDif = Vector2.zero - sm.owner.rb.linearVelocity;
        Vector2 movement = speedDif * sm.owner.movementType.runDeccelAmount;

        sm.owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
