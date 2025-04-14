using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("MovementSO")]
    public MovementSO movementType;

    public PlayerInput input { get; private set; }
    public Rigidbody2D rb { get; private set; }

    private PlayerStateMachine movementStateMachine;

    private void Awake()
    {
        movementStateMachine = new PlayerStateMachine(this);

        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.movementState);

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
