using UnityEngine;

public class MeleeAnimationParameterData
{
    private string horizontalParameterName = "Horizontal";
    private string verticalParameterName = "Vertical";
    private string trackParameterName = "IsTrack";
    private string strafeParameterName = "IsStrafe";
    private string strafeIsClockWiseParameterName = "IsClockWise";
    private string fleeParameterName = "IsFlee";
    private string attackParameterName = "IsAttack";

    public string attackAnimationName { get; private set; } = "Attack";

    public int horizontalParameterHash { get; private set; }
    public int verticalParameterHash { get; private set; }
    public int trackParameterHash { get; private set; }
    public int strafeParameterHash { get; private set; }
    public int strafeIsClockWiseParameterHash { get; private set; }
    public int fleeParameterHash { get; private set; }
    public int attackParameterHash { get; private set; }    
    
    public MeleeAnimationParameterData()
    {
        horizontalParameterHash = Animator.StringToHash(horizontalParameterName);
        verticalParameterHash = Animator.StringToHash(verticalParameterName);
        trackParameterHash = Animator.StringToHash(trackParameterName);

        strafeParameterHash = Animator.StringToHash(strafeParameterName);
        strafeIsClockWiseParameterHash = Animator.StringToHash(strafeIsClockWiseParameterName);
        fleeParameterHash = Animator.StringToHash(fleeParameterName);
        attackParameterHash = Animator.StringToHash(attackParameterName);
    }
}
