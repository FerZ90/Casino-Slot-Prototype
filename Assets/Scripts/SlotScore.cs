using System.Collections.Generic;
using UnityEngine;

public partial class SlotScore : MonoBehaviour
{
    [SerializeField] private LineScore[] scoreList;

    private List<SlotID> _rowScore;
    public List<SlotID> RowScore => _rowScore;

    public void SetScore()
    {
        _rowScore = new List<SlotID>();

        foreach (var line in scoreList)
        {
            if (line.SlotScore == null)
                continue;

            _rowScore.Add(new SlotID(line.SlotScore.SlotID, line.SlotScore.transform));
            Debug.Log($"LAST SCORE: {line.SlotScore.SlotID}");
        }
    }


}
