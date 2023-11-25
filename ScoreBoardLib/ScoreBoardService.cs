namespace ScoreBoardLib;

public class ScoreBoardService : IScoreBoard
{
    private readonly IMatchRepository matchRepository;
    private readonly ISortingStrategy sortingStrategy;

    public ScoreBoardService(IMatchRepository matchRepository, ISortingStrategy sortingStrategy)
    {
        this.matchRepository = matchRepository ?? throw new ArgumentNullException(nameof(matchRepository));
        this.sortingStrategy = sortingStrategy ?? throw new ArgumentNullException(nameof(sortingStrategy));
    }

    public void StartGame(string homeTeam, string awayTeam)
    {
        if (string.IsNullOrWhiteSpace(homeTeam) || string.IsNullOrWhiteSpace(awayTeam))
        {
            throw new ArgumentException("Team names cannot be null or empty.");
        }

        var match = new Match
        {
            HomeTeam = homeTeam,
            AwayTeam = awayTeam,
            HomeScore = 0,
            AwayScore = 0,
            StartTime = DateTime.Now
        };

        matchRepository.AddMatch(match);
    }

    public void FinishGame(string homeTeam, string awayTeam)
    {
        var match = matchRepository.GetMatch(homeTeam, awayTeam);
        if (match != null)
        {
            matchRepository.RemoveMatch(match);
        }
    }

    public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
    {
        var match = matchRepository.GetMatch(homeTeam, awayTeam);
        if (match != null)
        {
            match.HomeScore = homeScore;
            match.AwayScore = awayScore;
        }
    }

    public IEnumerable<Match> GetSummary()
    {
        var matches = matchRepository.GetAllMatches();
        return sortingStrategy.Sort(matches);
    }
}