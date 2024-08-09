using System;

public static class Tween<T> where T : ITweenAnimation
{
    public static T GetAnimation()
    {
        var newAnimation = Activator.CreateInstance<T>();
        AnimationUpdater.Instance.SetupAnimation(newAnimation);
        return newAnimation;
    }
}
