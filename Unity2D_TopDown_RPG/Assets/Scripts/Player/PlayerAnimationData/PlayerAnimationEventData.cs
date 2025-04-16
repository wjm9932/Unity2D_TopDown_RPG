using UnityEngine;

public class PlayerAnimationEventData
{
    public float dodgeFinishTime { get; private set; } = 1f;
 
    public float attack_1FinishTime { get; private set; } = 0.8f;
    public float attack_1HitTime { get; private set; } = 0.425f;
    public float attack_2InputEnableTime { get; private set; } = 0.7f;

    
    public float attack_2FinishTime { get; private set; } = 1f;
}
