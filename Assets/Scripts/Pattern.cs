using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Pattern
{
    public List<Vector2Int> pattern;

    private List<SlotID> _validIDs;

    private bool _isValid;
    public bool IsValid => _isValid;

    public Pattern(List<Vector2Int> pattern)
    {
        this.pattern = pattern;
        _validIDs = new List<SlotID>();
        _isValid = true;
    }

    public void Validate(SlotID slotID)
    {
        Debug.Log($"Validate_00");

        if (!_isValid)
            return;

        Debug.Log($"Validate_01");

        if (_validIDs.Count == 0)
        {
            _validIDs.Add(slotID);
            return;
        }

        Debug.Log($"Validate_02");

        if (slotID.ID == _validIDs[0].ID)
            _validIDs.Add(slotID);
        else
            _isValid = false;
    }

    public void Reset()
    {
        _validIDs = new List<SlotID>();
        _isValid = true;
    }

    public List<ScoreCounter> GetFinalScore()
    {
        var result = new List<ScoreCounter>();

        Debug.Log($"_validIDs Count--> {_validIDs.Count}");

        var positions = _validIDs.Select(id => id.Transform.position).ToList();
        int score = ScoreGlobalValues.GetScore(_validIDs[0].ID, _validIDs.Count);

        return result;
    }
}



