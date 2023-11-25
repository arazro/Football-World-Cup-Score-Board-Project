using Moq;

namespace ScoreBoardLib.Tests;

public class ScoreBoardServiceTests
{
    private readonly Mock<IMatchRepository> mockRepository;
    private readonly Mock<ISortingStrategy> mockSortingStrategy;
    private readonly Mock<IScoreBoardValidationService> validationService;
    private readonly ScoreBoardService scoreBoardService;

    public ScoreBoardServiceTests()
    {
        mockRepository = new Mock<IMatchRepository>();
        mockSortingStrategy = new Mock<ISortingStrategy>();
        validationService = new Mock<IScoreBoardValidationService>();
        scoreBoardService = new ScoreBoardService(mockRepository.Object, mockSortingStrategy.Object, validationService.Object);
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
    public void StartGame_WithValidInput_ShouldAddMatch()
    {
        // Arrange
        string homeTeam = "HomeTeam";
        string awayTeam = "AwayTeam";

        // Act
        scoreBoardService.StartGame(homeTeam, awayTeam);

        // Assert
        mockRepository.Verify(r => r.AddMatch(It.IsAny<Match>()), Times.Once);
    }

    [Fact]
    public void StartGame_WithInvalidTeamNames_ShouldThrowArgumentException()
    {
        validationService.Setup(v => v.ValidateTeamNames(It.IsAny<string>(), It.IsAny<string>()))
                             .Throws(new ArgumentException(MessageCenter.InvalidTeamNames));

        Assert.Throws<ArgumentException>(() => scoreBoardService.StartGame("", "AwayTeam"));
        Assert.Throws<ArgumentException>(() => scoreBoardService.StartGame("HomeTeam", ""));

        validationService.Verify(v => v.ValidateTeamNames(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
    }

    [Fact]
    public void FinishGame_ExistingGame_ShouldRemoveMatch()
    {
        // Arrange
        string homeTeam = "HomeTeam";
        string awayTeam = "AwayTeam";
        Match match = new Match(homeTeam, awayTeam, 0, 0, DateTime.Now);
        mockRepository.Setup(r => r.GetMatch(homeTeam, awayTeam)).Returns(match);

        // Act
        scoreBoardService.FinishGame(homeTeam, awayTeam);

        // Assert
        mockRepository.Verify(r => r.RemoveMatch(match), Times.Once);
    }

    [Fact]
    public void FinishGame_NonExistingGame_ShouldNotThrowException()
    {
        // Setup the repository to return null for a non-existing game
        mockRepository.Setup(repo => repo.GetMatch(It.IsAny<string>(), It.IsAny<string>())).Returns((Match)null);

        // Act
        Exception ex = Record.Exception(() => scoreBoardService.FinishGame("NonExistentHome", "NonExistentAway"));

        // Assert
        Assert.Null(ex); // No exception should be thrown
    }

    [Fact]
    public void UpdateScore_ShouldUpdateScoresOfMatch()
    {
        // Arrange
        string homeTeam = "HomeTeam";
        string awayTeam = "AwayTeam";
        var match = new Match(homeTeam, awayTeam, 0, 0, DateTime.Now);
        mockRepository.Setup(repo => repo.GetMatch(homeTeam, awayTeam)).Returns(match);

        // Act
        scoreBoardService.UpdateScore(homeTeam, awayTeam, 3, 2);

        // Assert
        Assert.Equal(3, match.HomeScore);
        Assert.Equal(2, match.AwayScore);
    }

    [Fact]
    public void UpdateScore_WithInvalidTeamNames_ShouldThrowArgumentException()
    {
        validationService.Setup(v => v.ValidateTeamNames(It.IsAny<string>(), It.IsAny<string>()))
                              .Throws(new ArgumentException(MessageCenter.InvalidTeamNames));

        // Act & Assert for empty or null home team name
        Assert.Throws<ArgumentException>(() => scoreBoardService.UpdateScore("", "AwayTeam", 1, 0));
        Assert.Throws<ArgumentException>(() => scoreBoardService.UpdateScore(null, "AwayTeam", 1, 0));

        // Act & Assert for empty or null away team name
        Assert.Throws<ArgumentException>(() => scoreBoardService.UpdateScore("HomeTeam", "", 1, 0));
        Assert.Throws<ArgumentException>(() => scoreBoardService.UpdateScore("HomeTeam", null, 1, 0));

        // Verify that the validation service is being called with the provided parameters
        validationService.Verify(v => v.ValidateTeamNames(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(4));
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
}