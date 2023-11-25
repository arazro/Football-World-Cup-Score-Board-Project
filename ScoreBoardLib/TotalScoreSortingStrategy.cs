namespace ScoreBoardLib;

public class TotalScoreSortingStrategy : ISortingStrategy
{
    public IEnumerable<Match> Sort(IEnumerable<Match> matches)
    {
        return matches.OrderByDescending(m => m.HomeScore + m.AwayScore)
                      .ThenByDescending(m => m.StartTime);
    }
}