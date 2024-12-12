using Xunit;

public class RockPaperScissorsTests
{
    [Fact]
    public void TestDetermineWinner_Tie()
    {
        int move1 = 1; // Roca
        int move2 = 1; // Roca

        int result = RockPaperScissors.DetermineWinner(move1, move2);

        Assert.Equal(0, result);
    }

    [Fact]
    public void TestDetermineWinner_Player1Wins()
    {
        Assert.Equal(1, RockPaperScissors.DetermineWinner(1, 3)); // Roca gana a Tijeras
        Assert.Equal(1, RockPaperScissors.DetermineWinner(2, 1)); // Papel gana a Roca
        Assert.Equal(1, RockPaperScissors.DetermineWinner(3, 2)); // Tijeras gana a Papel
    }

    [Fact]
    public void TestDetermineWinner_Player2Wins()
    {
        Assert.Equal(-1, RockPaperScissors.DetermineWinner(1, 2)); // Roca pierde con Papel
        Assert.Equal(-1, RockPaperScissors.DetermineWinner(2, 3)); // Papel pierde con Tijeras
        Assert.Equal(-1, RockPaperScissors.DetermineWinner(3, 1)); // Tijeras pierde con Roca
    }

    [Fact]
    public void TestGameEndsAfterTwoWins()
    {
        RockPaperScissors.ResetScores();

        RockPaperScissors.UpdateScore(1);
        RockPaperScissors.UpdateScore(1);

        bool isGameOver = RockPaperScissors.CheckGameOver();

        Assert.True(isGameOver);
    }

    [Fact]
    public void TestGameExtendsOnTieAfterThreeRounds()
    {
        RockPaperScissors.ResetScores();

        RockPaperScissors.UpdateScore(1);
        RockPaperScissors.UpdateScore(-1);

        bool isGameOver = RockPaperScissors.CheckGameOver(3);

        Assert.False(isGameOver);
    }
}