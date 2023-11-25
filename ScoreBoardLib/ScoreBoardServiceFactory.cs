namespace ScoreBoardLib;

public static class ScoreBoardServiceFactory
{
    public static IScoreBoard CreateScoreBoardService()
    {
        var matchRepository = new InMemoryMatchRepository();
        var sortingStrategy = new TotalScoreSortingStrategy();
        var scoreBoardValidation = new ScoreBoardValidationService();
        return new ScoreBoardService(matchRepository, sortingStrategy, scoreBoardValidation);
    }
}