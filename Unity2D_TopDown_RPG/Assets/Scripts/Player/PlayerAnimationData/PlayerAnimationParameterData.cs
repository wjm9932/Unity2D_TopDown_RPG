using UnityEngine;

public class PlayerAnimationParameterData
{
    private string horizontalParameterName = "Horizontal";
    private string verticalParameterName = "Vertical";
    private string idleParameterName = "IsIdle";
    private string runParameterName = "IsRun";
    private string dodgeParameterName = "IsDodge";
    private string attack_1ParameterName = "IsAttack_1";
    private string attack_2ParameterName = "IsAttack_2";
    private string dashAttackParameterName = "IsDashAttack";

    public int horizontalParameterHash { get; private set; }
    public int verticalParameterHash { get; private set; }
    public int idleParameterHash { get; private set; }
    public int runParameterHash { get; private set; }
    public int dodgeParameterHash { get; private set; }
    public int attack_1ParameterHash { get; private set; }
    public int attack_2ParameterHash { get; private set; }
    public int dashAttackParameterHash { get; private set; }

    public PlayerAnimationParameterData()
    {
        horizontalParameterHash = Animator.StringToHash(horizontalParameterName);
        verticalParameterHash = Animator.StringToHash(verticalParameterName);
        idleParameterHash = Animator.StringToHash(idleParameterName);
        runParameterHash = Animator.StringToHash(runParameterName);
        dodgeParameterHash = Animator.StringToHash(dodgeParameterName);
        attack_1ParameterHash = Animator.StringToHash(attack_1ParameterName);
        attack_2ParameterHash = Animator.StringToHash(attack_2ParameterName);
        dashAttackParameterHash = Animator.StringToHash(dashAttackParameterName);
    }
}
