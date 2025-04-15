using UnityEngine;

public class AnimationHandler<T> where T : IInitializable
{
    public Animator animator { get; private set; }
    public T animationData { get; private set; }

    public AnimationHandler(Animator animator, T animationData)
    {
        this.animator = animator;
        this.animationData = animationData;

        animationData.Initialize();
    }
}