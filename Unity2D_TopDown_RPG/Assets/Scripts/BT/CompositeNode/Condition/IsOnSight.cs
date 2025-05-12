using UnityEngine;

public class IsOnSight : ICondition
{
    private Blackboard blackboard;
    private MeleeEnemy owner;
    public IsOnSight(Blackboard blackboard)
    {
        this.blackboard = blackboard;
        owner = blackboard.GetData<MeleeEnemy>("owner");
    }
    public bool IsSatisfy()
    {
        return IsTargetOnSight();
    }

    private bool IsTargetOnSight()
    {
        bool isTargetVisible = false;

        var collider = Physics2D.OverlapCircle(owner.transform.position, owner.viewDistance, owner.targetLayer);

        if (collider != null)
        {
            var direction = (collider.transform.position - owner.transform.position).normalized;
            bool isInFieldOfView = Vector2.Dot(direction, owner.forward) >= Mathf.Cos(owner.fieldOfView * 0.5f * Mathf.Deg2Rad);

            if (isInFieldOfView == true)
            {
                var hit = Physics2D.Raycast(owner.transform.position, direction, owner.viewDistance);

                if (hit.collider != null && hit.transform.gameObject == collider.gameObject)
                {
                    isTargetVisible = true;
                    blackboard.SetData<Vector3>("spotPosition", collider.GetComponent<Player>().rootTransform.position);
                    owner.SetForward(collider.GetComponent<Player>().rootTransform.position);
                }
            }
        }

        return isTargetVisible;
    }
}
