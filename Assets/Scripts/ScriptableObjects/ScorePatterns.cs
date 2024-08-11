using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ScorePatterns", menuName = "ScriptableObjects/ScorePatterns", order = 4)]
public class ScorePatterns : ScriptableObject
{
    [SerializeField] private List<PatternsIndex> patternsIndex;
    [SerializeField] private List<ScoreColor> colors;

    public List<Pattern> GetPatterns()
    {
        List<Pattern> patterns = new List<Pattern>();

        for (int i = 0; i < patternsIndex.Count; i++)
        {
            patterns.Add(new Pattern(patternsIndex[i].pattern));
        }

        return patterns;
    }

    public Color GetScoreColor(Pattern pattern)
    {
        var patternFound = patternsIndex.FirstOrDefault(p => p.pattern == pattern.pattern);

        if (patternFound != null)
        {
            var colorFound = colors.FirstOrDefault(c => c.type == patternFound.scoreType);

            if (colorFound != null)
            {
                return colorFound.color;
            }
        }

        return Color.white;
    }

    public Gradient GetScoreGradientColor(Pattern pattern)
    {
        var color = GetScoreColor(pattern);

        var gradient = new Gradient();
        var colorKeys = new GradientColorKey[2];
        colorKeys[0] = new GradientColorKey(color, 0.0f);
        colorKeys[1] = new GradientColorKey(color, 1.0f);
        gradient.colorKeys = colorKeys;

        return gradient;
    }
}

[Serializable]
public class PatternsIndex : IEqualityComparer<PatternsIndex>
{
    public List<Vector2Int> pattern;
    public ScoreType scoreType;

    public bool Equals(PatternsIndex x, PatternsIndex y)
    {
        return false;
    }

    public int GetHashCode(PatternsIndex obj)
    {
        return pattern.GetHashCode();
    }
}

[Serializable]
public class ScoreColor
{
    public ScoreType type;
    public Color color;
}

public enum ScoreType
{
    Line,
    Spacing,
    Diagonal,
    Ladder
}
