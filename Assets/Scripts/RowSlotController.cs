using System.Collections.Generic;
using UnityEngine;

public class RowSlotController : MonoBehaviour
{
    [SerializeField] private RowSlotView[] rowViews;
    [SerializeField] private SlotView slotPrefab;

    public void InitializeRows(List<List<SlotModel>> rowsModels)
    {
        for (int i = 0; i < rowsModels.Count; i++)
        {
            if (i < 0 || i >= rowViews.Length)
                return;

            rowViews[i].SetupRow(slotPrefab, rowsModels[i]);
        }

    }
}
