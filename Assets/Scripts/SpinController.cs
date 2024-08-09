using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button spinButton;
    [SerializeField] private RowSlotView[] rowViews;
    [SerializeField] private SpinConfig spinConfig;
    public SpinConfig SpinConfig => spinConfig;

    private int rowCount;

    private static SpinController _instance;
    public static SpinController Instance => _instance;

    private SlotAnimation slotAnimation;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else
        {
            if (Application.isPlaying)
                Destroy(gameObject);
        }

        slotAnimation = Tween<SlotAnimation>.GetAnimation();
    }
    private void Start()
    {
        EventDispatcher.AddEventListener(EventNames.ON_STOP_ROW, OnFinishAnimation);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveEventListener(EventNames.ON_STOP_ROW, OnFinishAnimation);
    }

    public void SpinButton()
    {
        spinButton.interactable = false;
        rowCount = 0;
        slotAnimation.StartAnimation(rowViews[0].slots);

        EventDispatcher.DispatchEvent(EventNames.ON_START_SPIN);
    }

    private void OnFinishAnimation(object input)
    {
        if (input is not SlotAnimation)
            return;

        rowCount++;

        if (rowCount >= rowViews.Length)
        {
            EventDispatcher.DispatchEvent(EventNames.ON_FINISH_SPIN);
            spinButton.interactable = true;
            rowCount = 0;
            return;
        }

        slotAnimation.StartAnimation(rowViews[rowCount].slots);
    }



}
