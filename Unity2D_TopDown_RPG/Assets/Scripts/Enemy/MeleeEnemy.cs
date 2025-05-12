using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public Rigidbody2D rb { private set; get; }
    public ContextBasedSteeringAgent steeringAgent { private set; get; }
    public AnimationHandler<AnimationData<MeleeAnimationEventData, MeleeAnimationParameterData>> animationHandler { private set; get; }

    #region Movement Type
    [field: Header("Movement SO")]
    [field: SerializeField] public EnemyMovementSO movementSO { get; private set; }
    #endregion

    #region Steering 
    [Header("Steering Data")]
    [SerializeField] private int resolution;
    private bool isStrafing;
    private float strafeOffset;
    #endregion

    #region AI
    [field: Header("AI Data")]
    [field: SerializeField] public LayerMask obstacleLayer { private set; get; }
    [field: SerializeField] public LayerMask targetLayer { private set; get; }
    [field: SerializeField] public float fieldOfView { private set; get; }
    [field: SerializeField] public float viewDistance { private set; get; }
    public float stopDistance { private set; get; }

    
    #endregion

    public Vector2 forward { private set; get; } = new Vector2(0f, 1f);

    private BehaviorTree bt;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        steeringAgent = new ContextBasedSteeringAgent(resolution);
        animationHandler = new AnimationHandler<AnimationData<MeleeAnimationEventData, MeleeAnimationParameterData>>(GetComponentInChildren<Animator>(), 
            new AnimationData<MeleeAnimationEventData, MeleeAnimationParameterData>());
    }

    private void Start()
    {
        BuildBT();
        strafeOffset = Random.Range(0f, 3f);
        stopDistance = steeringAgent.rayLength + steeringAgent.castRadius + 0.1f + Random.Range(0f, 1f);
    }

    private void Update()
    {
        if(bt.blackboard.GetData<GameObject>("target") != null)
        {
            forward = (bt.blackboard.GetData<GameObject>("target").transform.position - transform.position).normalized;
        }

        steeringAgent.GetDangerWeight(transform.position, obstacleLayer);
        bt.root.Evaluate();
    }

    private void FixedUpdate()
    {
        bt.actionManager.ExecuteCurrentActionInFixedUpdate();
    }

    private void BuildBT()
    {
        Blackboard blackboard = new Blackboard();
        blackboard.SetData<MeleeEnemy>("owner", this);
        blackboard.SetData<GameObject>("target", null);
        blackboard.SetData<Vector3>("spotPosition", transform.position);

        bt = new BehaviorTreeBuilder(blackboard)
            .AddSelector()
                .AddSequence()
                    .AddCondition(new IsOnSight(blackboard))
                    .AddSelector()
                        .AddSequence()
                            .AddCondition(() => Vector2.Distance(transform.position, blackboard.GetData<GameObject>("target").transform.position) < 3f)
                            .AddAction(new Flee(blackboard))
                        .EndComposite()
                        .AddSequence()
                            .AddCondition(() => ShouldStrafe(isStrafing))
                            .AddAction(new Strafe(blackboard))
                        .EndComposite()
                    .EndComposite()
                .EndComposite()
                .AddSequence()
                    .AddCondition(() => Vector2.Distance(blackboard.GetData<Vector3>("spotPosition"), transform.position) > stopDistance)
                    .AddAction(new Track(blackboard))
                .EndComposite()
                .AddAction(new Idle(blackboard))
            .EndComposite()
            .Build();
    }
    private bool ShouldStrafe(bool currentState)
    {
        float d = Vector2.Distance(transform.position, bt.blackboard.GetData<GameObject>("target").transform.position);
        d -= strafeOffset;
        bool nextState = currentState;

        if (currentState)
        {
            if (d > 5f)
                nextState = false;
        }
        else
        {
            if (d <= 4f)
                nextState = true;
        }

        isStrafing = nextState;

        return nextState;
    }

    private void OnDrawGizmos()
    {
        if (steeringAgent == null || steeringAgent.directions == null || steeringAgent.final == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bt.blackboard.GetData<Vector3>("spotPosition"), 0.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, forward * 3f);

        Vector3 origin = transform.position;
        float radiusOffset = 0.5f;

        Gizmos.color = Color.cyan;
        DrawCircleFromDirections(origin, steeringAgent.castRadius);

        Gizmos.color = Color.gray;
        DrawCircleFromDirections(origin + Vector3.right * steeringAgent.rayLength, steeringAgent.castRadius);

        // Final 값 중 최대 인덱스 구하기
        int maxIndex = 0;
        float maxValue = float.MinValue;
        for (int i = 0; i < steeringAgent.final.Length; i++)
        {
            if (steeringAgent.final[i] > maxValue)
            {
                maxValue = steeringAgent.final[i];
                maxIndex = i;
            }
        }

        // 방향별 가중치 시각화
        for (int i = 0; i < steeringAgent.directions.Length; i++)
        {
            float weight = steeringAgent.final[i];
            Vector2 dir = steeringAgent.directions[i];
            Vector3 offsetOrigin = origin + (Vector3)(dir * radiusOffset);

            Gizmos.color = (i == maxIndex) ? Color.green : Color.red;
            Gizmos.DrawRay(offsetOrigin, (Vector3)(dir * weight * 2f));
        }
    }

    // directions 배열을 사용한 원형 시각화
    private void DrawCircleFromDirections(Vector3 center, float radius)
    {
        if (steeringAgent == null || steeringAgent.directions == null) return;

        Vector2[] dirs = steeringAgent.directions;

        Vector3 prevPoint = center + (Vector3)(dirs[0] * radius);
        for (int i = 1; i < dirs.Length; i++)
        {
            Vector3 nextPoint = center + (Vector3)(dirs[i] * radius);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
        // 마지막 점에서 첫 점으로 닫기
        Gizmos.DrawLine(prevPoint, center + (Vector3)(dirs[0] * radius));
    }
}
