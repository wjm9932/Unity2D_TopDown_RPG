using UnityEngine;

public class DodgeState : IState
{
    private PlayerStateMachine sm;
    private AnimationHandler<PlayerAnimationData> animationHandler;

    private Vector2 dodgeDir;
    private float currentForce;
    private const float decelerationFactor = 0.08f;
    private const float minForceThreshold = 8f;

    public DodgeState(PlayerStateMachine sm)
    {
        this.sm = sm;
        animationHandler = sm.owner.animationHandler;
    }

    public void Enter()
    {
        dodgeDir = sm.owner.lookDir.normalized;
        currentForce = sm.owner.movementType.dodgeForce;

        animationHandler.animator.SetBool(animationHandler.animationData.animParameterData.dodgeParameterHash, true);
    }

    public void FixedUpdate()
    {
        if (currentForce > minForceThreshold)
        {
            ApplyDodgeForce(currentForce);
        }
        else
        {
            ApplyDodgeForce(minForceThreshold);
        }

        Decelerate();
    }

    public void Update()
    {
        if (animationHandler.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationHandler.animationData.animEventTimeData.dodgeFinishTime)
        {
            sm.ChangeState(sm.runState);
        }
    }

    public void LateUpdate() { }

    public void Exit()
    {
        animationHandler.animator.SetBool(animationHandler.animationData.animParameterData.dodgeParameterHash, false);
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
        currentForce *= 1 - decelerationFactor;
    }

    private void ApplyDodgeForce(float force)
    {
        Vector2 targetSpeed = dodgeDir * force;
        Vector2 speedDiff = targetSpeed - sm.owner.rb.linearVelocity;

        sm.owner.rb.AddForce(speedDiff, ForceMode2D.Impulse);
    }
}
