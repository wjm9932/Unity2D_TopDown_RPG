using UnityEngine;

public class AnimationData<EventData, ParameterData> where EventData : new() where ParameterData : new()
{
    public EventData animEventTimeData { get; private set; }
    public ParameterData animParameterData { get; private set; }

    public AnimationData()
    {
        animEventTimeData = new EventData();
        animParameterData = new ParameterData(); ;
    }
}
