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
        Deaccel();
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
    }
    public void LateUpdate()
    {
    }
    public void Exit()
    {
    }
    private void Deaccel()
    {
        Vector2 speedDif = Vector2.zero - sm.owner.rb.linearVelocity;
        Vector2 movement = speedDif * sm.owner.movementType.runDeccelAmount;

        sm.owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
