using UnityEngine;

public class DodgeState : IState
{
    private PlayerStateMachine sm;
    private AnimHandler animationHandler;
    private Vector2 dodgeDir;
    public DodgeState(PlayerStateMachine sm)
    {
        this.sm = sm;
        animationHandler = sm.owner.animHandler;
    }
    public void Enter()
    {
        dodgeDir = sm.owner.lookDir;
        animationHandler.animator.SetBool(animationHandler.animationData.dodgeParameterHash, true);
    }
    public void FixedUpdate()
    {
        // If there are no obstacles in front, the velocity doesn't change direction or decrease in magnitude,
        // so speedDiff becomes zero, and no additional force is applied.
        // However, if there's an obstacle and the direction changes or the speed decreases,
        // the function compensates by reapplying force in the original dodgeDir direction.
        // At the same time, any force in an unintended direction caused by the collision
        // is neutralized through the speedDiff calculation.
        ApplyDodgeForce(sm.owner.movementType.dodgeForce); 
    }
    public void Update()
    {
        if(animationHandler.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationHandler.animationData.animEventTimeData.dodgeFinishTime)
        {
            sm.ChangeState(sm.idleState);
        }
    }
    public void LateUpdate()
    {

    }
    public void Exit()
    {
        animationHandler.animator.SetBool(animationHandler.animationData.dodgeParameterHash, false);
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

    private void ApplyDodgeForce(float force)
    {
        Vector2 targetSpeed = dodgeDir * force;
        Vector2 speedDiff = targetSpeed - sm.owner.rb.linearVelocity;

        sm.owner.rb.AddForce(speedDiff, ForceMode2D.Impulse);
    }
}