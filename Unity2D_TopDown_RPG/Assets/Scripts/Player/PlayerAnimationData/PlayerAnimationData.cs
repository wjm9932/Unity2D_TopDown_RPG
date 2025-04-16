using UnityEngine;

public class PlayerAnimationData
{
    public PlayerAnimationEventData animEventTimeData { get; private set; }
    public PlayerAnimationParameterData animParameterData { get; private set; }

    public PlayerAnimationData()
    {
        animEventTimeData = new PlayerAnimationEventData();
        animParameterData = new PlayerAnimationParameterData();
    }
}
