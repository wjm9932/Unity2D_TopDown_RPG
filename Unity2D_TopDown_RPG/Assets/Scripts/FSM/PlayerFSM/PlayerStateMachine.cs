using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player owner { get; private set; }

    public IdleState idleState { get; private set; }
    public RunState runState { get; private set; }
    public DodgeState dodgeState { get; private set; }
    public Attack_1State attack_1State { get; private set; }
    public Attack_2State attack_2State { get; private set; }
    public DashAttackState dashAttackState { get; private set; }

    public PlayerStateMachine(Player owner)
    {
        this.owner = owner;

        idleState = new IdleState(this);
        runState = new RunState(this);
        dodgeState = new DodgeState(this);
        attack_1State = new Attack_1State(this);
        attack_2State = new Attack_2State(this);
        dashAttackState = new DashAttackState(this);
    }
}
