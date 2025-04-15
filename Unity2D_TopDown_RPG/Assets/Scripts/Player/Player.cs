using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("MovementSO")]
    public MovementSO movementType;

    #region Animation
    [field: Header("Animator")]
    [field: SerializeField] public AnimHandler animHandler { get; private set; }
    #endregion
    #region Player Forwrad
    public Vector2 lookDir { get; private set; }
    #endregion


    public PlayerInput input { get; private set; }
    public Rigidbody2D rb { get; private set; }

    private PlayerStateMachine movementStateMachine;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        movementStateMachine = new PlayerStateMachine(this);
        animHandler.animationData.Initialize();
    }

    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.idleState);
    }

    private void Update()
    {
        SetForward(input.moveInput);

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

    private void SetForward(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.001f)
            lookDir = dir.normalized;
    }
}
