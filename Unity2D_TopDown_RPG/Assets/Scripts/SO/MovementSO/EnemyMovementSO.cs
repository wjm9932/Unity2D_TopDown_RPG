using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMovementSO", menuName = "Scriptable Objects/EnemyMovementSO")]
public class EnemyMovementSO : ScriptableObject
{
    [Header("Run")]
    public float runMaxSpeed; //Target speed we want the player to reach.
    [SerializeField] private float runAcceleration; //The speed at which our player accelerates to max speed, can be set to runMaxSpeed for instant acceleration down to 0 for none at all
    [HideInInspector] public float runAccelAmount { get; private set; } //The actual force (multiplied with speedDiff) applied to the player.

    [SerializeField] private float runDecceleration; //The speed at which our player decelerates from their current speed, can be set to runMaxSpeed for instant deceleration down to 0 for none at all
    [HideInInspector] public float runDeccelAmount { get; private set; } //Actual force (multiplied with speedDiff) applied to the player .

    [Header("Flee")]
    public float fleeMaxSpeed;
    [SerializeField] private float fleeAcceleration;
    [HideInInspector] public float fleeAccelAmount { get; private set; }

    [SerializeField] private float fleeDecceleration;
    [HideInInspector] public float fleeDeccelAmount { get; private set; }

    [Header("Strafe")]
    public float strafeMaxSpeed;
    [SerializeField] private float strafeAcceleration;
    [HideInInspector] public float strafeAccelAmount { get; private set; }

    [SerializeField] private float strafeDecceleration;
    [HideInInspector] public float strafeDeccelAmount { get; private set; }

    private void OnValidate()
    {
        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);

        runAccelAmount = ((1 / Time.fixedDeltaTime) * runAcceleration) / runMaxSpeed;
        runDeccelAmount = ((1 / Time.fixedDeltaTime) * runDecceleration) / runMaxSpeed;

        fleeAccelAmount = Mathf.Clamp(fleeAcceleration, 0.01f, fleeMaxSpeed);
        fleeDeccelAmount = Mathf.Clamp(fleeDecceleration, 0.01f, fleeMaxSpeed);

        fleeAccelAmount = ((1 / Time.fixedDeltaTime) * fleeAcceleration) / fleeMaxSpeed;
        fleeDeccelAmount = ((1 / Time.fixedDeltaTime) * fleeDecceleration) / fleeMaxSpeed;

        strafeAccelAmount = Mathf.Clamp(strafeAcceleration, 0.01f, strafeMaxSpeed);
        strafeDeccelAmount = Mathf.Clamp(strafeDecceleration, 0.01f, strafeMaxSpeed);

        strafeAccelAmount = ((1 / Time.fixedDeltaTime) * strafeAcceleration) / strafeMaxSpeed;
        strafeDeccelAmount = ((1 / Time.fixedDeltaTime) * strafeDecceleration) / strafeMaxSpeed;
    }
}
