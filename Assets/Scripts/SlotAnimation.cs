using System;
using UnityEngine;

public class SlotAnimation : ITweenAnimation
{
    public event Action<ITweenAnimation> OnstartAnimation;
    public event Action<ITweenAnimation> OnFinishAnimation;
    public event Action<ITweenAnimation> OnDestroyAnimation;

    private RectTransform[] _slots;

    private SlotAnimationConfig _config;
    private bool isSpining = false;
    private bool isBreaking = false;
    private float initialVelocity;
    private float spinTime;
    private float bottomPosition = 0;
    private RectTransform topSlot;
    private float height;

    private float timer;

    ~SlotAnimation()
    {
        OnDestroyAnimation?.Invoke(this);
    }

    public void StartAnimation(RectTransform[] slots)
    {
        _slots = slots;
        Reset();
        Start();
    }

    private void Reset()
    {
        var firstSlot = _slots[0];
        var lastSlot = _slots[0];

        for (int i = 1; i < _slots.Length; i++)
        {
            if (_slots[i].localPosition.y > firstSlot.localPosition.y)
                firstSlot = _slots[i];

            if (_slots[i].localPosition.y < lastSlot.localPosition.y)
                lastSlot = _slots[i];
        }

        topSlot = firstSlot;
        bottomPosition = lastSlot.localPosition.y + lastSlot.rect.yMin;
        height = topSlot.rect.height;
        _config = GameInstaller.Instance.spinConfig.animationConfig;
    }

    public void Start()
    {
        if (!isSpining)
        {
            initialVelocity = UnityEngine.Random.Range(_config.initialMinVelocity, _config.initialMaxVelocity);
            spinTime = UnityEngine.Random.Range(_config.spinMinTime, _config.spinMaxTime);
            isSpining = true;
            isBreaking = false;
            timer = 0f;
        }

        OnstartAnimation?.Invoke(this);
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (!isSpining)
            return;

        if (isBreaking)
        {
            if (initialVelocity > _config.initialMinVelocity * 0.6f)
                initialVelocity *= 0.99f;

            if (Mathf.Abs(topSlot.localPosition.y % height) < 5.0f)
            {
                initialVelocity = 0;
                isBreaking = false;
                isSpining = false;
                OnFinishAnimation?.Invoke(this);
                return;
            }

        }

        foreach (var slot in _slots)
        {
            slot.localPosition -= new Vector3(0, initialVelocity * Time.fixedDeltaTime, 0);
        }

        foreach (var slot in _slots)
        {
            if (slot.localPosition.y <= bottomPosition)
            {
                slot.localPosition = new Vector3(slot.localPosition.x, topSlot.localPosition.y + height, slot.localPosition.z);
                topSlot = slot;
            }
        }

        CheckForStopSpin();
    }

    private void CheckForStopSpin()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= spinTime)
            isBreaking = true;

    }
}
