using UnityEngine;

public class MovementState : IState
{
    private PlayerStateMachine sm;

    public MovementState(PlayerStateMachine sm)
    {
        this.sm = sm;
    }

    public void Enter()
    {

    }
    public void Update()
    {

    }
    public void FixedUpdate()
    {
        Run();

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

    private void Run()
    {
        Vector2 targetSpeed = sm.owner.input.moveInput * sm.owner.movementType.runMaxSpeed;
        
        float accelAmount;

        accelAmount = (Mathf.Abs(targetSpeed.sqrMagnitude) > 0.01f) ? sm.owner.movementType.runAccelAmount : sm.owner.movementType.runDeccelAmount;

        Vector2 speedDif = targetSpeed - sm.owner.rb.linearVelocity;
        Vector2 movement = speedDif * accelAmount;

        sm.owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
