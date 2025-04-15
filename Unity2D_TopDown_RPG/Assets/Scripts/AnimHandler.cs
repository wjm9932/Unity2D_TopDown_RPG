using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    public Animator animator { get; private set; }
    [field: SerializeField] public PlayerAnimationData animationData { get; private set; }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
