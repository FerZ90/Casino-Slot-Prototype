using System.Collections.Generic;
using UnityEngine;

public class RowSlotView : MonoBehaviour
{
    private List<RectTransform> _slots;
    public RectTransform[] Slots => _slots.ToArray();

    public void SetupRow(SlotView prefab, List<SlotModel> rowModel)
    {
        var childsSlots = transform.GetComponentsInChildren<SlotView>(true);

        foreach (var child in childsSlots)
            Destroy(child.gameObject);

        _slots = new List<RectTransform>();

        float height = prefab.GetComponent<RectTransform>().rect.height;
        float lastPos = +(height * ((int)(rowModel.Count / 2)));

        foreach (var model in rowModel)
        {
            var slotView = Instantiate(prefab, transform);
            slotView.UpdateView(model);

            var slotRect = slotView.GetComponent<RectTransform>();
            slotRect.localPosition = new Vector3(0, lastPos, 0);
            lastPos -= height;
            _slots.Add(slotRect);
        }
    }

}

