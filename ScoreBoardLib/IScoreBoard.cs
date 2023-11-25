namespace ScoreBoardLib;

public interface IScoreBoard
{
    void StartGame(string homeTeam, string awayTeam);

    void FinishGame(string homeTeam, string awayTeam);

    void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore);

    IEnumerable<Match> GetSummary();
}