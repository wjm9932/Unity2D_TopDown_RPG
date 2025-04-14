using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player owner { get; private set; }

    public MovementState movementState { get; private set; }

    public PlayerStateMachine(Player owner)
    {
        this.owner = owner;

        movementState = new MovementState(this);
    }
}
