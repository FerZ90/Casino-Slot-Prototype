using System.Collections.Generic;
using UnityEngine;

public partial class SlotScore : MonoBehaviour
{
    [SerializeField] private LineScore[] scoreList;

    private List<SlotID> slotsIDs;
    public List<SlotID> FinalScoreIDs => slotsIDs;

    private void Awake()
    {
        EventDispatcher.AddEventListener(EventNames.ON_STOP_ROW, WaitToFinishRowMovement);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveEventListener(EventNames.ON_STOP_ROW, WaitToFinishRowMovement);
    }

    private void WaitToFinishRowMovement(object animation)
    {
        if (animation is SlotAnimation)
        {
            if ((SlotAnimation)animation == GetComponent<SlotAnimation>())
            {
                slotsIDs = new List<SlotID>();

                foreach (var line in scoreList)
                {
                    if (line.SlotScore == null)
                        continue;

                    slotsIDs.Add(new SlotID(line.SlotScore.slotID, line.SlotScore.transform));
                    Debug.Log($"LAST SCORE: {line.SlotScore.slotID}");
                }
            }
        }
    }

}
