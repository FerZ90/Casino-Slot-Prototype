using UnityEngine;
using UnityEngine.UI;

public class SpinController : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private RowSlotView[] rowViews;

    private int rowCount;

    private SlotAnimation slotAnimation;

    private void Awake()
    {
        slotAnimation = Tween<SlotAnimation>.GetAnimation();
    }
    private void Start()
    {
        slotAnimation.OnFinishAnimation += OnFinishAnimation;
    }

    private void OnDestroy()
    {
        slotAnimation.OnFinishAnimation -= OnFinishAnimation;
    }

    public void SpinButton()
    {
        spinButton.interactable = false;
        rowCount = 0;
        slotAnimation.StartAnimation(rowViews[0].Slots);

        EventDispatcher.DispatchEvent(EventNames.ON_START_SPIN);
    }

    private void OnFinishAnimation(object input)
    {
        Debug.Log("OnFinishAnimation_00");

        if (input is not SlotAnimation)
            return;

        Debug.Log("OnFinishAnimation_01");

        rowViews[rowCount].StopRow();

        rowCount++;

        if (rowCount >= rowViews.Length)
        {
            EventDispatcher.DispatchEvent(EventNames.ON_FINISH_SPIN);
            spinButton.interactable = true;
            rowCount = 0;
            return;
        }

        slotAnimation.StartAnimation(rowViews[rowCount].Slots);
    }



}
