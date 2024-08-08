using System.Collections.Generic;
using System.Linq;

public class StraightLineScorer : IScorer
{
    public List<ScoreCounter> Score(SlotID[,] scoreSlots)
    {
        var result = new List<ScoreCounter>();

        for (int i = 0; i < scoreSlots.GetLength(0); i++)
        {
            List<SlotID> scoreIDs = new List<SlotID> { scoreSlots[i, 0] };

            for (int j = 1; j < scoreSlots.GetLength(1); j++)
            {
                UnityEngine.Debug.Log("");

                if (scoreIDs[scoreIDs.Count - 1].ID == scoreSlots[i, j].ID)
                {
                    UnityEngine.Debug.Log($"SCORE ID---> {scoreIDs[scoreIDs.Count - 1].ID}");
                    scoreIDs.Add(scoreSlots[i, j]);
                }
                else
                {
                    break;
                }
            }

            if (scoreIDs.Count > 1)
            {
                UnityEngine.Debug.Log("");

                var positions = scoreIDs.Select(id => id.Transform.position).ToList();
                int score = ScoreGlobalValues.scoreValues[scoreIDs[0].ID][scoreIDs.Count - 2];
                result.Add(new ScoreCounter() { positions = positions, score = score });
            }
        }

        return result;
    }
}
