using UnityEngine;

public class Strafe : IAction
{
    private Blackboard blackboard;
    private MeleeEnemy owner;

    private bool isClockWise;
    private float duration;
    public Strafe(Blackboard blackBoard)
    {
        this.blackboard = blackBoard;
        owner = blackboard.GetData<MeleeEnemy>("owner");
    }

    public void OnEnter()
    {
        Debug.Log("Enter Strafe");

        isClockWise = GetDirection();
        duration = GetRandomDuration();
    }

    public NodeState Execute()
    {
        if (duration <= 0f)
        {
            isClockWise = GetDirection();
            duration = GetRandomDuration();
        }

        duration -= Time.deltaTime;
        return NodeState.Running;
    }

    public void ExecuteInFixedUpdate()
    {
        GetInterestWeight();
        Move();
    }

    public void OnExit()
    {
    }

    void GetInterestWeight()
    {
        Vector2 toTarget = PerpendicularVector().normalized;

        for (int i = 0; i < owner.steeringAgent.directions.Length; i++)
        {
            float dot = Vector2.Dot(toTarget, owner.steeringAgent.directions[i].normalized);
            owner.steeringAgent.interest[i] = 1 + dot;
        }
    }

    private Vector2 PerpendicularVector()
    {
        Vector2 toTarget = blackboard.GetData<GameObject>("target").transform.position - owner.transform.position;

        if (isClockWise == true)
        {
            return new Vector2(toTarget.y, -toTarget.x);
        }
        else
        {
            return new Vector2(-toTarget.y, toTarget.x);
        }
    }

    private int GetFinalDirection()
    {
        int desiredDirIndex = 0;

        for (int i = 0; i < owner.steeringAgent.directions.Length; i++)
        {
            owner.steeringAgent.final[i] = owner.steeringAgent.interest[i] * (1f - owner.steeringAgent.danger[i]);
            if (owner.steeringAgent.final[i] > owner.steeringAgent.final[desiredDirIndex])
            {
                desiredDirIndex = i;
            }
        }

        return desiredDirIndex;
    }

    private void Move()
    {
        int dir = GetFinalDirection();

        Vector2 targetSpeed = owner.steeringAgent.directions[dir] * owner.movementSO.runMaxSpeed;

        Vector2 speedDif = targetSpeed - owner.rb.linearVelocity;
        Vector2 movement = speedDif * owner.movementSO.runAccelAmount;

        owner.rb.AddForce(movement, ForceMode2D.Force);
    }

    private bool GetDirection()
    {
        return Random.value < 0.5f;
    }

    private float GetRandomDuration()
    {
        return Random.Range(2f, 5f);
    }
}