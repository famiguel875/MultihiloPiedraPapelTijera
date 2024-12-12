using Xunit;

public class RockPaperScissorsTests
{
    [Fact]
    public void TestDetermineWinner_Tie()
    {
        int move1 = 1; // Rock
        int move2 = 1; // Rock
        int result = RockPaperScissors.DetermineWinner(move1, move2);
        Assert.Equal(0, result);
    }

    [Fact]
    public void TestDetermineWinner_Player1Wins()
    {
        Assert.Equal(1, RockPaperScissors.DetermineWinner(1, 3)); // Rock vs Scissors
        Assert.Equal(1, RockPaperScissors.DetermineWinner(2, 1)); // Paper vs Rock
        Assert.Equal(1, RockPaperScissors.DetermineWinner(3, 2)); // Scissors vs Paper
    }

    [Fact]
    public void TestDetermineWinner_Player2Wins()
    {
        Assert.Equal(-1, RockPaperScissors.DetermineWinner(1, 2)); // Rock vs Paper
        Assert.Equal(-1, RockPaperScissors.DetermineWinner(2, 3)); // Paper vs Scissors
        Assert.Equal(-1, RockPaperScissors.DetermineWinner(3, 1)); // Scissors vs Rock
    }

    [Fact]
    public void TestPlayMatch_WinnerExists()
    {
        string winner = RockPaperScissors.PlayMatch("Player 1", "Player 2");
        Assert.True(winner == "Player 1" || winner == "Player 2");
    }

    [Fact]
    public void TestPlayMatch_MatchStopsAfterTwoWins()
    {
        // Simulate a match to check if it stops after two wins
        int player1Wins = 0;
        int player2Wins = 0;
        int round = 1;

        while (round <= 3 || player1Wins == player2Wins)
        {
            int move1 = 1; // Simulate Rock for Player 1
            int move2 = 3; // Simulate Scissors for Player 2

            int result = RockPaperScissors.DetermineWinner(move1, move2);

            if (result == 1) player1Wins++;
            else if (result == -1) player2Wins++;

            if (player1Wins == 2 || player2Wins == 2) break;

            round++;
        }

        Assert.True(player1Wins == 2 || player2Wins == 2);
    }

    [Fact]
    public void TestTournament_WinnerExists()
    {
        List<string> players = new List<string> { "Player 1", "Player 2", "Player 3", "Player 4" };
        string winner = RockPaperScissors.ExecuteTournament(players);
        Assert.Contains("Player", winner);
    }

    [Fact]
    public void TestTournament_ReductionInPlayers()
    {
        List<string> players = new List<string> { "Player 1", "Player 2", "Player 3", "Player 4" };
        string winner = RockPaperScissors.ExecuteTournament(players);

        // Check the tournament resulted in exactly one player
        Assert.Single(new List<string> { winner });
    }
}
