using System.Collections.Generic;

public static class ScoreGlobalValues
{
    private static Dictionary<SlotsIDs, List<int>> scoreValues = new Dictionary<SlotsIDs, List<int>>()
         {
            { SlotsIDs.Bell, new List<int>{ 25, 50, 75, 100 } },
            { SlotsIDs.Cherry, new List<int>{ 5, 10, 20, 40 } },
            { SlotsIDs.Blueberry, new List<int>{ 1, 2, 5, 10 } },
            { SlotsIDs.Watermelon, new List<int>{ 10, 20, 30, 60 } },
            { SlotsIDs.Orange, new List<int>{ 5, 10, 15, 30 } },
            { SlotsIDs.Grape, new List<int>{ 5, 10, 20, 50} },
            { SlotsIDs.Lemon, new List<int>{ 2, 5, 10, 20} }
        };

    public static int GetScore(SlotsIDs ID, int count)
    {
        int finalCount = count - 2;

        if (finalCount < 0)
            return -1;

        return scoreValues[ID][finalCount];
    }
}
