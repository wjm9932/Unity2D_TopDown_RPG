using UnityEngine;

[CreateAssetMenu(fileName = "MovementSO", menuName = "Scriptable Objects/MovementSO")]
public class MovementSO : ScriptableObject
{
    [Header("Run")]
    public float runMaxSpeed; //Target speed we want the player to reach.
    [SerializeField] private float runAcceleration; //The speed at which our player accelerates to max speed, can be set to runMaxSpeed for instant acceleration down to 0 for none at all
    [HideInInspector] public float runAccelAmount { get; private set; } //The actual force (multiplied with speedDiff) applied to the player.

    [SerializeField] private float runDecceleration; //The speed at which our player decelerates from their current speed, can be set to runMaxSpeed for instant deceleration down to 0 for none at all
    [HideInInspector] public float runDeccelAmount { get; private set; } //Actual force (multiplied with speedDiff) applied to the player .


    private void OnValidate()
    {
        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);

        runAccelAmount = ((1 / Time.fixedDeltaTime) * runAcceleration) / runMaxSpeed;
        runDeccelAmount = ((1 / Time.fixedDeltaTime) * runDecceleration) / runMaxSpeed;
    }
}

