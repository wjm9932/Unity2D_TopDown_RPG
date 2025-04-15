using UnityEngine;

public class IdleState : IState
{
    private PlayerStateMachine sm;
    private AnimHandler animationHandler;

    public IdleState(PlayerStateMachine sm)
    {
        this.sm = sm;
        animationHandler = sm.owner.animHandler;
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
        if(sm.owner.input.moveInput.sqrMagnitude > 0.01f)
        {
            sm.ChangeState(sm.runState);
        }
        if (sm.owner.input.isDodge == true)
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

    private void Deaccel()
    {
        Vector2 speedDif = Vector2.zero - sm.owner.rb.linearVelocity;
        Vector2 movement = speedDif * sm.owner.movementType.runDeccelAmount;

        sm.owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
