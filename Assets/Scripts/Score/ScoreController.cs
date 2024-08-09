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
        scorers = new List<IScorer>() { new StraightLineScorer(), new DiagonalBlankScorer() };
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
        foreach (var score in rowsScore)
            score.GetRowScore();

        finalSlots = new SlotID[3, rowsScore.Length];

        for (int i = 0; i < finalSlots.GetLength(0); i++)
        {
            for (int j = 0; j < finalSlots.GetLength(1); j++)
            {
                finalSlots[i, j] = rowsScore[j].RowScore[i];
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
