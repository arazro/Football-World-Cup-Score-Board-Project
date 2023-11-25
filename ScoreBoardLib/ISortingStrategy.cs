namespace ScoreBoardLib;

public interface ISortingStrategy
{
    IEnumerable<Match> Sort(IEnumerable<Match> matches);
}