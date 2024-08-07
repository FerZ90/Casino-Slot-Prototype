using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineScorePrefab;
    [SerializeField] private SlotScore[] rowsScore;

    private SlotID[,] finalSlots;

    private List<IScorer> scorers;

    private List<LineRenderer> scoreLines = new List<LineRenderer>();

    private void Start()
    {
        scorers = new List<IScorer>() { new StraightLineScorer() };
        EventDispatcher.AddEventListener(EventNames.ON_FINISH_SPIN, CalculateScore);
        EventDispatcher.AddEventListener(EventNames.ON_START_SPIN, ResetLines);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveEventListener(EventNames.ON_FINISH_SPIN, CalculateScore);
        EventDispatcher.RemoveEventListener(EventNames.ON_START_SPIN, ResetLines);
    }

    private void ResetLines(object input)
    {
        for (int i = 0; i < scoreLines.Count; i++)
        {
            Destroy(scoreLines[i].gameObject);
        }

        scoreLines.Clear();
    }

    public void CalculateScore(object input)
    {
        finalSlots = new SlotID[3, rowsScore.Length];

        for (int i = 0; i < finalSlots.GetLength(0); i++)
        {
            for (int j = 0; j < finalSlots.GetLength(1); j++)
            {
                finalSlots[i, j] = rowsScore[j].FinalScoreIDs[i];
            }
        }

        int finalScore = 0;

        foreach (var scorer in scorers)
        {
            var scoreCounter = scorer.Score(finalSlots);

            foreach (var counter in scoreCounter)
            {
                var lineScoreGO = Instantiate(lineScorePrefab);
                scoreLines.Add(lineScoreGO);
                lineScoreGO.transform.position = Vector3.zero;
                var camPos = counter.positions.Select(p => new Vector3(p.x, p.y, -1)).ToArray();
                lineScoreGO.positionCount = camPos.Length;
                lineScoreGO.SetPositions(camPos);

                finalScore += counter.score;
            }
        }

        Debug.Log($"<color=cyan>FINAL SCORE: {finalScore}</color>");
    }
}

public interface IScorer
{
    List<ScoreCounter> Score(SlotID[,] scoreSlots);
}

public class StraightLineScorer : IScorer
{
    public Dictionary<SlotsIDs, List<int>> scoreValues = new Dictionary<SlotsIDs, List<int>>()
         {
            { SlotsIDs.Bell, new List<int>{ 25, 50, 75, 100 } },
            { SlotsIDs.Cherry, new List<int>{ 5, 10, 20, 40 } },
            { SlotsIDs.Blueberry, new List<int>{ 1, 2, 5, 10 } },
            { SlotsIDs.Watermelon, new List<int>{ 10, 20, 30, 60 } },
            { SlotsIDs.Orange, new List<int>{ 5, 10, 15, 30 } },
            { SlotsIDs.Grape, new List<int>{ 5, 10, 20, 50} },
            { SlotsIDs.Lemon, new List<int>{ 2, 5, 10, 20} }
        };

    public List<ScoreCounter> Score(SlotID[,] scoreSlots)
    {
        var result = new List<ScoreCounter>();

        for (int i = 0; i < scoreSlots.GetLength(0); i++)
        {
            List<SlotID> scoreIDs = new List<SlotID> { scoreSlots[i, 0] };

            for (int j = 1; j < scoreSlots.GetLength(1); j++)
            {
                if (scoreIDs[scoreIDs.Count - 1].ID == scoreSlots[i, j].ID)
                    scoreIDs.Add(scoreSlots[i, j]);
                else
                    break;
            }

            if (scoreIDs.Count > 1)
            {
                var positions = scoreIDs.Select(id => id.Transform.position).ToList();
                int score = scoreValues[scoreIDs[0].ID][scoreIDs.Count - 2];
                result.Add(new ScoreCounter() { positions = positions, score = score });
            }
        }

        return result;
    }
}

public struct ScoreCounter
{
    public List<Vector3> positions;
    public int score;
}