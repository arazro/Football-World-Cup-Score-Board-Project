namespace ScoreBoardLib;

public class InMemoryMatchRepository : IMatchRepository
{
    private readonly List<Match> matches = new List<Match>();

    public void AddMatch(Match match)
    {
        matches.Add(match);
    }

    public void RemoveMatch(Match match)
    {
        matches.Remove(match);
    }

    public Match GetMatch(string homeTeam, string awayTeam)
    {
        return matches.FirstOrDefault(m => m.HomeTeam == homeTeam && m.AwayTeam == awayTeam);
    }

    public IEnumerable<Match> GetAllMatches()
    {
        return matches.AsReadOnly();
    }
}