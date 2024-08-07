using UnityEngine;

public class LineScore : MonoBehaviour
{
    private SlotView _slotView;
    public SlotView SlotScore => _slotView;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<SlotView>(out var slotView))
            _slotView = slotView;
    }
}
