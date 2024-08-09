using UnityEngine;
using UnityEngine.UI;

public class SlotView : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    private SlotModel _model;
    public SlotsIDs SlotID => _model.slotID;

    public void UpdateView(SlotModel model)
    {
        _model = model;
    }
}

public class SlotModel
{
    public SlotsIDs slotID;
    public Sprite icon;
}
