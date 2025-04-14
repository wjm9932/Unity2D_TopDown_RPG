using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("MovementSO")]
    public MovementSO movementType;

    #region Animation
    [Header("Animator")]
    [field: SerializeField] public AnimHandler animHandler { get; private set; }
    [field : SerializeField] public PlayerAnimationData animationData { get; private set; }
    #endregion

    public PlayerInput input { get; private set; }
    public Rigidbody2D rb { get; private set; }

    private PlayerStateMachine movementStateMachine;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        animationData.Initialize();
    }

    private void Start()
    {
        movementStateMachine = new PlayerStateMachine(this);
        movementStateMachine.ChangeState(movementStateMachine.runState);
    }

    private void Update()
    {
        movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
        movementStateMachine.FixedUpdate();
    }

    private void LateUpdate()
    {
        movementStateMachine.LateUpdate();
    }
}
