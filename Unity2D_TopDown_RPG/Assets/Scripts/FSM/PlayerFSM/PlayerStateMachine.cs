using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player owner { get; private set; }

    public IdleState idleState { get; private set; }
    public RunState runState { get; private set; }
    public DodgeState dodgeState { get; private set; }

    public PlayerStateMachine(Player owner)
    {
        this.owner = owner;

        idleState = new IdleState(this);
        runState = new RunState(this);
        dodgeState = new DodgeState(this);
    }
}
