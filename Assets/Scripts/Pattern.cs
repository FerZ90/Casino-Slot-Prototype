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
        if (!_isValid)
            return;

        if (_validIDs.Count == 0)
        {
            _validIDs.Add(slotID);
            return;
        }

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
        var positions = _validIDs.Select(id => id.Transform.position).ToList();
        int score = ScoreGlobalValues.GetScore(_validIDs[0].ID, _validIDs.Count);

        return result;
    }
}



