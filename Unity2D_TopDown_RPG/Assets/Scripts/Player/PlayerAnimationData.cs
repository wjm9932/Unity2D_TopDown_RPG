using UnityEngine;

[System.Serializable]
public class PlayerAnimationData
{
    [field: Header("Animation Event Time Data")]
    [field: SerializeField] public PlayerAnimEventSO animEventTimeData { get; private set; }

    [Header("Movement Group Parameter Names")]
    private string horizontalParameterName = "Horizontal";
    private string verticalParameterName = "Vertical";
    private string idleParameterName = "IsIdle";
    private string runParameterName = "IsRun";
    private string dodgeParameterName = "IsDodge";

    public int horizontalParameterHash { get; private set; }
    public int verticalParameterHash { get; private set; }
    public int idleParameterHash { get; private set; }
    public int runParameterHash { get; private set; }
    public int dodgeParameterHash { get; private set; }

    public void Initialize()
    {
        horizontalParameterHash = Animator.StringToHash(horizontalParameterName);
        verticalParameterHash = Animator.StringToHash(verticalParameterName);
        idleParameterHash = Animator.StringToHash(idleParameterName);
        runParameterHash = Animator.StringToHash(runParameterName);
        dodgeParameterHash = Animator.StringToHash(dodgeParameterName);
    }
}
