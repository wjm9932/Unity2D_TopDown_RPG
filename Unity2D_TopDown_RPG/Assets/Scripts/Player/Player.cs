using UnityEngine;

public class Player : MonoBehaviour
{
    #region Movement 
    [Header("MovementSO")]
    public MovementSO movementType;
    #endregion

    #region Animation
    public AnimationHandler<PlayerAnimationData> animationHandler { get; private set; }
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

        animationHandler = new AnimationHandler<PlayerAnimationData>(GetComponentInChildren<Animator>(), new PlayerAnimationData());
        movementStateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.idleState);
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

    public void SetForward(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.001f)
            lookDir = dir.normalized;
    }
}
