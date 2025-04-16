using UnityEngine;

public class DashAttackState : IState
{
    private AnimationHandler<PlayerAnimationData> animationHandler;
    private PlayerStateMachine sm;

    private Vector2 dashDir;
    private float currentDashForce;
    private float dashAttackTime;

    private const float dashForce = 80f; // I dont know which value is better between 70 and 80. Im still testing
    private const float decelerationFactor = 0.2f;
    private readonly float targetDashAttackTime;

    public DashAttackState(PlayerStateMachine sm)
    {
        this.sm = sm;
        animationHandler = sm.owner.animationHandler;
        targetDashAttackTime = Utility.CalculateTimeUntilVelocityBelow(dashForce, decelerationFactor, 3.16f);
    }

    public void Enter()
    {
        dashDir = sm.owner.lookDir;
        dashAttackTime = targetDashAttackTime;
        currentDashForce = dashForce;

        animationHandler.animator.SetBool(animationHandler.animationData.animParameterData.dashAttackParameterHash, true);
    }

    public void FixedUpdate()
    {
        ApplyDodgeForce(currentDashForce);
        Decelerate();
    }
    public void Update()
    {
        if (dashAttackTime <= 0)
        {
            sm.ChangeState(sm.runState);
        }
        dashAttackTime -= Time.deltaTime;
    }
    public void LateUpdate()
    {
    }
    public void Exit()
    {
        animationHandler.animator.SetBool(animationHandler.animationData.animParameterData.dashAttackParameterHash, false);
    }

    private void Decelerate()
    {
        // The reason I keep calling this function is to maintain the initial dodge direction.
        // If there are no obstacles in front, the velocity doesn't change direction or decrease in magnitude,
        // so speedDiff becomes zero, and no additional force is applied.
        // However, if there's an obstacle and the direction changes or the speed decreases,
        // the function compensates by reapplying force in the original dodgeDir direction.
        // At the same time, any force in an unintended direction caused by the collision
        // is neutralized through the speedDiff calculation.
        currentDashForce *= 1 - decelerationFactor;
    }

    private void ApplyDodgeForce(float force)
    {
        Vector2 targetSpeed = dashDir * force;
        Vector2 speedDiff = targetSpeed - sm.owner.rb.linearVelocity;

        sm.owner.rb.AddForce(speedDiff, ForceMode2D.Impulse);
    }
}