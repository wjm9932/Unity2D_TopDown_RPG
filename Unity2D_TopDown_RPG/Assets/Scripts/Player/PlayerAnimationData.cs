using UnityEngine;

[System.Serializable]
public class PlayerAnimationData
{

    [Header("Movement Group Parameter Names")]
    [Space()]
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string runParameterName = "Run";
    [SerializeField] private string horizontalParameterName = "Horizontal";
    [SerializeField] private string verticalParameterName = "Vertical";

    public int idleParameterHash { get; private set; }
    public int runParameterHash { get; private set; }
    public int horizontalParameterHash { get; private set; }
    public int verticalParameterHash { get; private set; }

    public void Initialize()
    {
        idleParameterHash = Animator.StringToHash(idleParameterName);
        runParameterHash = Animator.StringToHash(runParameterName);
        horizontalParameterHash = Animator.StringToHash(horizontalParameterName);
        verticalParameterHash = Animator.StringToHash(verticalParameterName);
    }
}
