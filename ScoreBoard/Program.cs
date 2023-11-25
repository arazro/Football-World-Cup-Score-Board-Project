using ScoreBoardLib;

public class Program
{
    public static void Main(string[] args)
    {
        // Setting up dependencies
        IMatchRepository matchRepository = new InMemoryMatchRepository();
        ISortingStrategy sortingStrategy = new TotalScoreSortingStrategy();

        // Creating ScoreBoardService with dependencies
        IScoreBoard scoreBoard = new ScoreBoardService(matchRepository, sortingStrategy);

        // Sample data
        scoreBoard.StartGame("Mexico", "Canada");
        scoreBoard.UpdateScore("Mexico", "Canada", 0, 5);

        scoreBoard.StartGame("Spain", "Brazil");
        scoreBoard.UpdateScore("Spain", "Brazil", 10, 2);

        scoreBoard.StartGame("Germany", "France");
        scoreBoard.UpdateScore("Germany", "France", 2, 2);

        scoreBoard.StartGame("Uruguay", "Italy");
        scoreBoard.UpdateScore("Uruguay", "Italy", 6, 6);

        scoreBoard.StartGame("Argentina", "Australia");
        scoreBoard.UpdateScore("Argentina", "Australia", 3, 1);

        // Displaying summary
        Console.WriteLine("Football World Cup Score Board:");
        Console.WriteLine("--------------------------------");
        foreach (var match in scoreBoard.GetSummary())
        {
            Console.WriteLine($"{match.HomeTeam} {match.HomeScore} - {match.AwayTeam} {match.AwayScore}");
        }

        // Example of finishing a game
        Console.WriteLine("\nFinishing the game between Germany and France...");
        scoreBoard.FinishGame("Germany", "France");

        // Displaying updated summary
        Console.WriteLine("\nUpdated Score Board:");
        Console.WriteLine("---------------------");
        foreach (var match in scoreBoard.GetSummary())
        {
            Console.WriteLine($"{match.HomeTeam} {match.HomeScore} - {match.AwayTeam} {match.AwayScore}");
        }
    }
}