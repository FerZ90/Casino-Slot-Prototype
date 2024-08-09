using System;
using UnityEngine;

public class SlotAnimation : ITweenAnimation
{
    public event Action<ITweenAnimation> OnstartAnimation;
    public event Action<ITweenAnimation> OnFinishAnimation;

    private RectTransform[] _slots;

    private SlotAnimationConfig _config;
    private bool isSpining = false;
    private bool isBreaking = false;
    private float initialVelocity;
    private float spinTime;
    private float bottomPosition;
    private RectTransform topSlot;
    private float height;

    private float timer;

    public void StartAnimation(RectTransform[] slots)
    {
        _slots = slots;
        height = _slots[0].rect.height;
        bottomPosition = _slots[_slots.Length - 1].localPosition.y + _slots[_slots.Length - 1].rect.yMin;
        _config = SpinController.Instance.SpinConfig.animationConfig;
        topSlot = _slots[0];
        Start();
    }

    public void Start()
    {
        OnstartAnimation?.Invoke(this);

        if (!isSpining)
        {
            initialVelocity = UnityEngine.Random.Range(_config.initialMinVelocity, _config.initialMaxVelocity);
            spinTime = UnityEngine.Random.Range(_config.spinMinTime, _config.spinMaxTime);
            isSpining = true;
            timer = 0f;
        }
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (isSpining)
        {
            if (isBreaking)
            {
                if (initialVelocity > _config.initialMinVelocity * 0.5f)
                    initialVelocity *= 0.99f;

                if (Mathf.Abs(_slots[0].localPosition.y % height) < 5.0f)
                {
                    initialVelocity = 0;
                    isBreaking = false;
                    isSpining = false;
                    EventDispatcher.DispatchEvent(EventNames.ON_STOP_ROW, this);
                    //OnFinishAnimation?.Invoke(this);
                    return;
                }
            }

            foreach (var slot in _slots)
            {
                slot.localPosition -= new Vector3(0, initialVelocity * Time.fixedDeltaTime, 0);
            }

            foreach (var slot in _slots)
            {
                if ((slot.localPosition.y - slot.rect.yMin) <= bottomPosition)
                {
                    slot.localPosition = new Vector3(slot.localPosition.x, topSlot.localPosition.y + height, slot.localPosition.z);
                    topSlot = slot;
                }
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
