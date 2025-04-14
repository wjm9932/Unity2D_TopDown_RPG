using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    public Animator animator { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
