using UnityEngine;
public class ContextBasedSteeringAgent
{
    public Vector2[] directions { private set; get; }
    public float[] interest { private set; get; }
    public float[] danger { private set; get; }
    public float[] final { private set; get; }

    public float rayLength { private set; get; }
    public float castRadius { private set; get; }

    public ContextBasedSteeringAgent(int resolution, float rayLength = 1f, float castRadius = 1f)
    {
        directions = Utility.GenerateDirections(resolution);
        interest = new float[resolution];
        danger = new float[resolution];
        final = new float[resolution];

        this.rayLength = rayLength;
        this.castRadius = castRadius;
    }

    public void GetDangerWeight(Vector3 origin, LayerMask layer)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            Vector2 dir = directions[i];

            RaycastHit2D hit = Physics2D.CircleCast(origin, castRadius, dir, rayLength, layer);

            if (hit.collider != null)
            {
                float dist = hit.distance;
                float dangerValue = 1f - (dist / rayLength);
                danger[i] = dangerValue;
            }
            else
            {
                danger[i] = 0f;
            }
        }
    }
}
