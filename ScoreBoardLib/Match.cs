﻿namespace ScoreBoardLib;

public class Match
{
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
    public DateTime StartTime { get; set; }
}