namespace ScoreBoardLib;

public class ScoreBoardService : IScoreBoard
{
    private readonly IMatchRepository matchRepository;
    private readonly ISortingStrategy sortingStrategy;
    private readonly IScoreBoardValidationService validationService;

    public ScoreBoardService(IMatchRepository matchRepository, ISortingStrategy sortingStrategy, IScoreBoardValidationService validationService)
    {
        this.matchRepository = matchRepository;
        this.sortingStrategy = sortingStrategy;
        this.validationService = validationService;
    }

    public void StartGame(string homeTeam, string awayTeam)
    {
        validationService.ValidateTeamNames(homeTeam, awayTeam);
        var match = new Match(homeTeam, awayTeam, 0, 0, DateTime.Now);
        matchRepository.AddMatch(match);
    }

    public void FinishGame(string homeTeam, string awayTeam)
    {
        validationService.ValidateTeamNames(homeTeam, awayTeam);

        var match = matchRepository.GetMatch(homeTeam, awayTeam);
        if (match != null)
        {
            matchRepository.RemoveMatch(match);
        }
        else
        {
            Console.WriteLine(MessageCenter.NonExistentGame);
        }
    }

    public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
    {
        validationService.ValidateTeamNames(homeTeam, awayTeam);
        validationService.ValidateScores(homeScore, awayScore);

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