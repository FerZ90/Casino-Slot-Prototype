using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Pattern
{
    public List<Vector2Int> pattern;

    private List<SlotID> _validIDs;

    public Pattern(List<Vector2Int> pattern)
    {
        Debug.Log($"pattern count: {pattern.Count}");
        this.pattern = pattern;
        _validIDs = new List<SlotID>();
    }

    private void Validate(SlotID[,] finalSlots)
    {
        for (int i = 0; i < pattern.Count; i++)
        {
            var current = finalSlots[pattern[i].x, pattern[i].y];

            Debug.Log($"Validate_00: {current.ID}");

            if (_validIDs.Count == 0)
            {
                Debug.Log($"Validate_01: {current.ID}");
                _validIDs.Add(current);
                continue;
            }

            if (_validIDs[0].ID == current.ID)
            {
                Debug.Log($"Validate_02: {current.ID}");
                _validIDs.Add(current);
            }
            else
            {
                break;
            }
        }
    }

    public void Reset()
    {
        _validIDs = new List<SlotID>();
    }

    public List<ScoreCounter> GetFinalScore(SlotID[,] finalSlots)
    {
        Validate(finalSlots);

        var result = new List<ScoreCounter>();

        var positions = _validIDs.Select(id => id.Transform.position).ToList();
        int score = ScoreGlobalValues.GetScore(_validIDs[0].ID, _validIDs.Count);

        return result;
    }
}



