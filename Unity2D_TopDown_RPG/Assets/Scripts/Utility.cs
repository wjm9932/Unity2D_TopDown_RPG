using UnityEngine;

public static class Utility
{
    public static float CalculateTimeUntilVelocityBelow(float initialForce, float decelerationFactor, float velocityThreshold)
    {
        if (decelerationFactor <= 0f || decelerationFactor >= 1f)
        {
            Debug.LogError("Invalid decelerationFactor");
            return -1f;
        }

        float threshold = velocityThreshold;
        float velocity = initialForce;
        float time = 0f;

        while (velocity >= threshold)
        {
            velocity *= (1 - decelerationFactor);
            time += Time.fixedDeltaTime;
        }

        return time;
    }

}
