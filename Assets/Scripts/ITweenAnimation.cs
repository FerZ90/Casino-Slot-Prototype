using System;

public interface ITweenAnimation
{
    public event Action<ITweenAnimation> OnstartAnimation;
    public event Action<ITweenAnimation> OnFinishAnimation;
    public event Action<ITweenAnimation> OnDestroyAnimation;

    void Start();
    void Update();
    void FixedUpdate();
}
