using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScorePatterns", menuName = "ScriptableObjects/ScorePatterns", order = 4)]
public class ScorePatterns : ScriptableObject
{
    public List<Pattern> patterns;
}
