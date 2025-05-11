using UnityEngine;

public static class Utility
{
    private static Vector2[] animationDirections;
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

    public static Vector2[] GenerateDirections(int resolution)
    {
        Vector2[] directions = new Vector2[resolution];
        float angleStep = 360f / resolution;
        for (int i = 0; i < resolution; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            directions[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
        return directions;
    }

    private static Vector2[] InitializeAnimationDirections()
    {
        return GenerateDirections(8);
    }

    public static Vector2[] GetAnimationDirections()
    {
        if(animationDirections == null)
        {
            animationDirections = InitializeAnimationDirections();
        }

        return animationDirections;
    }

    public static int GetClosestDirectionIndex(Vector2[] directions, Vector2 target)
    {
        Vector2 normalizedTarget = target.normalized;

        float maxDot = -1f;
        int closestIndex = 0;

        for (int i = 0; i < directions.Length; i++)
        {
            float dot = Vector2.Dot(normalizedTarget, directions[i].normalized);

            if (dot > maxDot)
            {
                maxDot = dot;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}
