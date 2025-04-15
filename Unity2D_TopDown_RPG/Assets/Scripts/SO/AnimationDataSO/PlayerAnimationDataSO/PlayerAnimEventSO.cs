using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimEventSO", menuName = "Scriptable Objects/PlayerAnimEventSO")]
public class PlayerAnimEventSO : ScriptableObject
{
    [field: Header("Dodge")]
    [field: SerializeField] public float dodgeFinishTime { get; private set; } = 1f;

    [field: Header("Attack")]
    [field: SerializeField] public float attack_1FinishTime { get; private set; } = 0.8f;
    [field: SerializeField] public float attack_1AttackTriggerTime { get; private set; } = 0.425f;
}
