using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScorePatterns", menuName = "ScriptableObjects/ScorePatterns", order = 4)]
public class ScorePatterns : ScriptableObject
{
    [SerializeField] private List<PatternsIndex> patternsIndex;

    public List<Pattern> GetPatterns()
    {
        List<Pattern> patterns = new List<Pattern>();

        for (int i = 0; i < patternsIndex.Count; i++)
        {
            patterns.Add(new Pattern(patternsIndex[i].pattern));
        }

        return patterns;
    }
}

[Serializable]
public class PatternsIndex
{
    public List<Vector2Int> pattern;
}
