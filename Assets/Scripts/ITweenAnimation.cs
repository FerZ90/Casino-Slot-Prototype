using System;

public interface ITweenAnimation
{
    public event Action<ITweenAnimation> OnstartAnimation;

    public event Action<ITweenAnimation> OnFinishAnimation;

    void Start();
    void Update();
    void FixedUpdate();
}
