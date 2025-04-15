using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimEventSO", menuName = "Scriptable Objects/PlayerAnimEventSO")]
public class PlayerAnimEventSO : ScriptableObject
{
    [field: SerializeField] public float dodgeFinishTime { get; private set; } = 1f;
}
