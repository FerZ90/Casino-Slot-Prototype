using System.Collections;
using UnityEngine;

public class SlotAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform[] slots;

    private SlotAnimationConfig _config;
    private bool isSpining = false;
    private bool isBreaking = false;
    private float initialVelocity;
    private float spinTime;
    private float bottomPosition;
    private RectTransform topSlot;

    private float height;

    private void Start()
    {
        height = slots[0].rect.height;
        bottomPosition = slots[slots.Length - 1].localPosition.y + slots[slots.Length - 1].rect.yMin;
        _config = SpinController.Instance.SpinConfig.animationConfig;
        topSlot = slots[0];
    }

    public void Animate()
    {
        if (!isSpining)
        {
            initialVelocity = Random.Range(_config.initialMinVelocity, _config.initialMaxVelocity);
            spinTime = Random.Range(_config.spinMinTime, _config.spinMaxTime);
            isSpining = true;
            StartCoroutine(StopSpin());
        }
    }

    private void FixedUpdate()
    {
        if (isSpining)
        {
            if (isBreaking)
            {
                if (initialVelocity > _config.initialMinVelocity * 0.5f)
                    initialVelocity *= 0.99f;

                if (Mathf.Abs(slots[0].localPosition.y % height) < 5.0f)
                {
                    initialVelocity = 0;
                    isBreaking = false;
                    isSpining = false;
                    EventDispatcher.DispatchEvent(EventNames.ON_STOP_ROW, this);
                    return;
                }
            }

            foreach (var slot in slots)
            {
                slot.localPosition -= new Vector3(0, initialVelocity * Time.fixedDeltaTime, 0);
            }

            foreach (var slot in slots)
            {
                if ((slot.localPosition.y - slot.rect.yMin) <= bottomPosition)
                {
                    slot.localPosition = new Vector3(slot.localPosition.x, topSlot.localPosition.y + height, slot.localPosition.z);
                    topSlot = slot;
                }
            }

        }
    }


    private IEnumerator StopSpin()
    {
        yield return new WaitForSeconds(spinTime);
        isBreaking = true;
    }

}
