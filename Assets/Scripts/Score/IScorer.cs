using System.Collections.Generic;

public interface IScorer
{
    List<ScoreCounter> Score(SlotID[,] scoreSlots);
}
