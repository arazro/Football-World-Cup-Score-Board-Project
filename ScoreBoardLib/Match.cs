namespace ScoreBoardLib;

public class Match
{
    public Match(string homeTeam, string awayTeam, int homeScore, int awayScore, DateTime startTime)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        HomeScore = homeScore;
        AwayScore = awayScore;
        StartTime = startTime;
    }

    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
    public DateTime StartTime { get; set; }
}