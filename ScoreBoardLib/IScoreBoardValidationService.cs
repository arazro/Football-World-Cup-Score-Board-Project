namespace ScoreBoardLib;

public interface IScoreBoardValidationService
{
    void ValidateTeamNames(string homeTeam, string awayTeam);

    void ValidateScores(int homeScore, int awayScore);
}