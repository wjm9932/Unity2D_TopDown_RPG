using UnityEngine;

public class IdleState : IState
{
    private AnimationHandler<PlayerAnimationData> animationHandler;
    private PlayerStateMachine sm;

    public IdleState(PlayerStateMachine sm)
    {
        this.sm = sm;
        animationHandler = sm.owner.animationHandler;
    }

    public void Enter()
    {
    }
    public void FixedUpdate()
    {
        Decelerate();
    }
    public void Update()
    {
        if (sm.owner.input.moveInput.sqrMagnitude > 0.01f)
        {
            sm.ChangeState(sm.runState);
        }
        if (sm.owner.input.dodgeBufferTime > 0f)
        {
            sm.ChangeState(sm.dodgeState);
        }
        if(sm.owner.input.autoAttackBufferTime > 0f)
        {
            sm.ChangeState(sm.attack_1State);
        }
        if (sm.owner.input.dashAttackBufferTime > 0f)
        {
            sm.ChangeState(sm.dashAttackState);
        }
    }
    public void LateUpdate()
    {
    }
    public void Exit()
    {
    }
    private void Decelerate()
    {
        Vector2 speedDif = Vector2.zero - sm.owner.rb.linearVelocity;
        Vector2 movement = speedDif * sm.owner.movementType.runDeccelAmount;

        sm.owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
