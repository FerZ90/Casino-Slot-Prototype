using System.Collections.Generic;
using System.Linq;

public class DiagonalBlankScorer : IScorer
{
    private readonly int[] list1 = new int[2] { 2, 0 };
    private readonly int[] list2 = new int[2] { 0, 1 };
    private readonly int[] list3 = new int[2] { 2, 2 };
    private readonly int[] list4 = new int[2] { 0, 3 };
    private readonly int[] list5 = new int[2] { 2, 4 };

    private readonly int[] list6 = new int[2] { 0, 0 };
    private readonly int[] list7 = new int[2] { 2, 1 };
    private readonly int[] list8 = new int[2] { 0, 2 };
    private readonly int[] list9 = new int[2] { 2, 3 };
    private readonly int[] list10 = new int[2] { 0, 4 };

    private int[][] pattern1;
    private int[][] pattern2;

    public DiagonalBlankScorer()
    {
        pattern1 = new int[][] { list1, list2, list3, list4, list5 };
        pattern2 = new int[][] { list6, list7, list8, list9, list10 };
    }

    public List<ScoreCounter> Score(SlotID[,] scoreSlots)
    {
        var result = new List<ScoreCounter>();

        List<SlotID> scoreIDs = new List<SlotID> { scoreSlots[pattern1[0][0], pattern1[0][1]] };

        for (int i = 1; i < pattern1.Length; i++)
        {
            var scoreSlot = scoreSlots[pattern1[i][0], pattern1[i][1]];

            if (scoreSlot.ID == scoreIDs[scoreIDs.Count - 1].ID)
                scoreIDs.Add(scoreSlot);
            else
                break;
        }

        if (scoreIDs.Count > 1)
        {
            var positions = scoreIDs.Select(id => id.Transform.position).ToList();
            int score = ScoreGlobalValues.scoreValues[scoreIDs[0].ID][scoreIDs.Count - 2];
            result.Add(new ScoreCounter() { positions = positions, score = score });
        }

        scoreIDs.Clear();
        scoreIDs = new List<SlotID> { scoreSlots[pattern2[0][0], pattern2[0][1]] };

        for (int i = 1; i < pattern2.Length; i++)
        {
            var scoreSlot = scoreSlots[pattern2[i][0], pattern2[i][1]];

            if (scoreSlot.ID == scoreIDs[scoreIDs.Count - 1].ID)
                scoreIDs.Add(scoreSlot);
            else
                break;
        }

        if (scoreIDs.Count > 1)
        {
            var positions = scoreIDs.Select(id => id.Transform.position).ToList();
            int score = ScoreGlobalValues.scoreValues[scoreIDs[0].ID][scoreIDs.Count - 2];
            result.Add(new ScoreCounter() { positions = positions, score = score });
        }

        return result;
    }
}
