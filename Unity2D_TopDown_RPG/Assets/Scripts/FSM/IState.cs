
public interface IState
{
    public void Enter();
    public void FixedUpdate();
    public void Update();
    public void LateUpdate();
    public void Exit();
    public void OnAnimationEnterEvent();
    public void OnAnimationExitEvent();
    public void OnAnimationTransitionEvent();
    public void OnAnimatorIK();
}
