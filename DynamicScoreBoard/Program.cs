using ScoreBoardLib;

public class Program
{
    public static void Main()
    {
        IMatchRepository matchRepository = new InMemoryMatchRepository();
        ISortingStrategy sortingStrategy = new TotalScoreSortingStrategy();
        var scoreBoardValidation = new ScoreBoardValidationService();
        IScoreBoard scoreBoard = new ScoreBoardService(matchRepository, sortingStrategy, scoreBoardValidation);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nFootball World Cup Score Board:");
            Console.WriteLine("1. Start a Game");
            Console.WriteLine("2. Update Score");
            Console.WriteLine("3. Finish a Game");
            Console.WriteLine("4. Display Score Board");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    StartGame(scoreBoard);
                    break;

                case 2:
                    UpdateScore(scoreBoard);
                    break;

                case 3:
                    FinishGame(scoreBoard);
                    break;

                case 4:
                    DisplayScoreBoard(scoreBoard);
                    break;

                case 5:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private static void StartGame(IScoreBoard scoreBoard)
    {
        try
        {
            Console.Write("Enter Home Team Name: ");
            string homeTeam = Console.ReadLine();
            Console.Write("Enter Away Team Name: ");
            string awayTeam = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(homeTeam) || string.IsNullOrWhiteSpace(awayTeam))
            {
                throw new ArgumentException("Team names cannot be empty.");
            }

            scoreBoard.StartGame(homeTeam, awayTeam);
            Console.WriteLine("Game started successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void UpdateScore(IScoreBoard scoreBoard)
    {
        try
        {
            Console.Write("Enter Home Team Name: ");
            string homeTeam = Console.ReadLine();
            Console.Write("Enter Away Team Name: ");
            string awayTeam = Console.ReadLine();
            Console.Write("Enter Home Team Score: ");
            int homeScore = int.Parse(Console.ReadLine());
            Console.Write("Enter Away Team Score: ");
            int awayScore = int.Parse(Console.ReadLine());

            scoreBoard.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);
            Console.WriteLine("Score updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void FinishGame(IScoreBoard scoreBoard)
    {
        try
        {
            Console.Write("Enter Home Team Name: ");
            string homeTeam = Console.ReadLine();
            Console.Write("Enter Away Team Name: ");
            string awayTeam = Console.ReadLine();

            scoreBoard.FinishGame(homeTeam, awayTeam);
            Console.WriteLine("Game finished successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void DisplayScoreBoard(IScoreBoard scoreBoard)
    {
        Console.WriteLine("\nCurrent Score Board:");
        foreach (var match in scoreBoard.GetSummary())
        {
            Console.WriteLine($"{match.HomeTeam} {match.HomeScore} - {match.AwayTeam} {match.AwayScore}");
        }
    }
}