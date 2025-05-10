using UnityEngine;
public class Strafe : IAction
{
    private Blackboard blackboard;
    private MeleeEnemy owner;
    private bool isClockWise;
    private float duration;
    // 방향 전환 결정을 위한 임계값
    private const float DIRECTION_SWITCH_THRESHOLD = 0.1f;

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
        GetInterestWeightAndCheckDirectionSwitch();
        Move();
    }

    public void OnExit()
    {
    }

    void GetInterestWeightAndCheckDirectionSwitch()
    {
        float currentDirectionPriority = CalculateDirectionPriorityForDirection(isClockWise);
        float oppositeDirectionPriority = CalculateDirectionPriorityForDirection(!isClockWise);

        if (oppositeDirectionPriority - currentDirectionPriority >= DIRECTION_SWITCH_THRESHOLD)
        {
            isClockWise = !isClockWise;
            Debug.Log("Strafe direction switched due to obstacle. New direction: " + (isClockWise ? "Clockwise" : "Counter-Clockwise"));
        }

        SetInterestWeightForCurrentDirection();
    }

    private void SetInterestWeightForCurrentDirection()
    {
        Vector2 toTarget = PerpendicularVector(isClockWise).normalized;
        for (int i = 0; i < owner.steeringAgent.directions.Length; i++)
        {
            float dot = Vector2.Dot(toTarget, owner.steeringAgent.directions[i].normalized);
            owner.steeringAgent.interest[i] = 1 + dot;
        }
    }

    private float CalculateDirectionPriorityForDirection(bool clockwise)
    {
        Vector2 toTarget = PerpendicularVector(clockwise).normalized;

        for (int i = 0; i < owner.steeringAgent.directions.Length; i++)
        {
            float dot = Vector2.Dot(toTarget, owner.steeringAgent.directions[i].normalized);
            owner.steeringAgent.interest[i] = 1 + dot;
        }

        float totalPriority = 0f;
        for (int i = 0; i < owner.steeringAgent.directions.Length; i++)
        {
            totalPriority += owner.steeringAgent.interest[i] * (1f - owner.steeringAgent.danger[i]);
        }

        return totalPriority / owner.steeringAgent.directions.Length;
    }

    private Vector2 PerpendicularVector(bool clockwise)
    {
        Vector2 toTarget = blackboard.GetData<GameObject>("target").transform.position - owner.transform.position;
        if (clockwise)
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
        Vector2 targetSpeed = owner.steeringAgent.directions[dir] * owner.movementSO.strafeMaxSpeed;
        Vector2 speedDif = targetSpeed - owner.rb.linearVelocity;
        Vector2 movement = speedDif * owner.movementSO.strafeAccelAmount;
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