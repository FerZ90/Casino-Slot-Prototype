using System.Collections.Generic;
using UnityEngine;

public class AnimationUpdater : MonoBehaviour
{
    private List<ITweenAnimation> _animations;

    private static AnimationUpdater _instance;
    public static AnimationUpdater Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        _animations = new List<ITweenAnimation>();
    }

    public void SetupAnimation(ITweenAnimation animation)
    {
        animation.OnstartAnimation += AddAnimation;
        animation.OnFinishAnimation += RemoveAnimation;
    }

    private void AddAnimation(ITweenAnimation animation)
    {
        if (animation != null && !_animations.Contains(animation))
        {
            _animations.Add(animation);
            animation.OnstartAnimation -= AddAnimation;
        }
    }

    private void RemoveAnimation(ITweenAnimation animation)
    {
        if (animation != null && _animations.Contains(animation))
        {
            _animations.Remove(animation);
            animation.OnFinishAnimation -= RemoveAnimation;
        }
    }

    private void Update()
    {
        if (_animations == null)
            return;

        for (int i = 0; i < _animations.Count; i++)
        {
            _animations[i].Update();
        }
    }

    private void FixedUpdate()
    {
        if (_animations == null)
            return;

        for (int i = 0; i < _animations.Count; i++)
        {
            _animations[i].FixedUpdate();
        }
    }
}
