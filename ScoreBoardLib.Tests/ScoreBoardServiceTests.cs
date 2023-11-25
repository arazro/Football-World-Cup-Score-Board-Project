using Moq;

namespace ScoreBoardLib.Tests;

public class ScoreBoardServiceTests
{
    private readonly Mock<IMatchRepository> mockRepository;
    private readonly Mock<ISortingStrategy> mockSortingStrategy;
    private readonly ScoreBoardService scoreBoardService;

    public ScoreBoardServiceTests()
    {
        mockRepository = new Mock<IMatchRepository>();
        mockSortingStrategy = new Mock<ISortingStrategy>();
        scoreBoardService = new ScoreBoardService(mockRepository.Object, mockSortingStrategy.Object);
    }

    [Fact]
    public void StartGame_ShouldAddNewGameWithInitialScores()
    {
        // Arrange
        string homeTeam = "HomeTeam";
        string awayTeam = "AwayTeam";

        // Act
        scoreBoardService.StartGame(homeTeam, awayTeam);

        // Assert
        mockRepository.Verify(repo => repo.AddMatch(It.Is<Match>(
            m => m.HomeTeam == homeTeam && m.AwayTeam == awayTeam && m.HomeScore == 0 && m.AwayScore == 0)),
            Times.Once);
    }

    [Fact]
    public void FinishGame_ShouldRemoveGame()
    {
        // Arrange
        string homeTeam = "HomeTeam";
        string awayTeam = "AwayTeam";
        var match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam };
        mockRepository.Setup(repo => repo.GetMatch(homeTeam, awayTeam)).Returns(match);

        // Act
        scoreBoardService.FinishGame(homeTeam, awayTeam);

        // Assert
        mockRepository.Verify(repo => repo.RemoveMatch(match), Times.Once);
    }

    [Fact]
    public void UpdateScore_ShouldUpdateScoresOfMatch()
    {
        // Arrange
        string homeTeam = "HomeTeam";
        string awayTeam = "AwayTeam";
        var match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam };
        mockRepository.Setup(repo => repo.GetMatch(homeTeam, awayTeam)).Returns(match);

        // Act
        scoreBoardService.UpdateScore(homeTeam, awayTeam, 3, 2);

        // Assert
        Assert.Equal(3, match.HomeScore);
        Assert.Equal(2, match.AwayScore);
    }

    [Fact]
    public void UpdateScore_NonExistingGame_ShouldHandleGracefully()
    {
        // Arrange
        string homeTeam = "NonExistingHome";
        string awayTeam = "NonExistingAway";
        mockRepository.Setup(repo => repo.GetMatch(homeTeam, awayTeam)).Returns((Match)null);

        // Act & Assert
        Exception ex = Record.Exception(() => scoreBoardService.UpdateScore(homeTeam, awayTeam, 1, 1));
        Assert.Null(ex); // No exception should be thrown
    }

    [Fact]
    public void GetSummary_ShouldReturnSortedMatches()
    {
        // Arrange
        var matches = new List<Match> { /* ... populate test matches ... */ };
        mockRepository.Setup(repo => repo.GetAllMatches()).Returns(matches);
        mockSortingStrategy.Setup(strategy => strategy.Sort(matches)).Returns(matches); // Assuming sorting is done correctly

        // Act
        var summary = scoreBoardService.GetSummary();

        // Assert
        Assert.Equal(matches, summary);
        mockSortingStrategy.Verify(strategy => strategy.Sort(matches), Times.Once);
    }

    [Fact]
    public void StartGame_WithInvalidTeamNames_ShouldThrowArgumentException()
    {
        // Act & Assert for empty home team name
        Assert.Throws<ArgumentException>(() => scoreBoardService.StartGame("", "AwayTeam"));

        // Act & Assert for empty away team name
        Assert.Throws<ArgumentException>(() => scoreBoardService.StartGame("HomeTeam", ""));
    }
}