namespace ScoreBoardLib;

public class ScoreBoardValidationService : IScoreBoardValidationService
{
    public void ValidateTeamNames(string homeTeam, string awayTeam)
    {
        if (string.IsNullOrEmpty(homeTeam) || string.IsNullOrEmpty(awayTeam))
            throw new ArgumentException(MessageCenter.InvalidTeamNames);
    }

    public void ValidateScores(int homeScore, int awayScore)
    {
        if (homeScore < 0 || awayScore < 0)
            throw new ArgumentException(MessageCenter.InvalidScores);
    }
}