using UnityEngine;

public class Idle : IAction
{
    private Blackboard blackboard;
    private MeleeEnemy owner;
    public Idle(Blackboard blackBoard)
    {
        this.blackboard = blackBoard;
        owner = blackboard.GetData<MeleeEnemy>("owner");
    }

    public void OnEnter()
    {

    }
    public NodeState Execute()
    {
        return NodeState.Running;
    }

    public void ExecuteInFixedUpdate()
    {
        Move();
    }
    public void OnExit()
    {
    }

    private void Move()
    {
        Vector2 speedDif = Vector2.zero - owner.rb.linearVelocity;
        Vector2 movement = speedDif * owner.movementSO.runDeccelAmount;

        owner.rb.AddForce(movement, ForceMode2D.Force);
    }
}
