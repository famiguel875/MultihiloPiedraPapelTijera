public class RockPaperScissors
{
    private const int ROCK = 1;
    private const int PAPER = 2;
    private const int SCISSORS = 3;

    private static readonly ThreadLocal<Random> threadRandom = new ThreadLocal<Random>(() => new Random());
    private static readonly object lockObj = new object();

    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Rock-Paper-Scissors Tournament!");

        // Create 16 players
        List<string> players = Enumerable.Range(1, 16).Select(i => $"Player {i}").ToList();

        // Execute the tournament
        string winner = ExecuteTournament(players);

        Console.WriteLine($"The tournament winner is: {winner}");
    }

    public static string ExecuteTournament(List<string> players)
    {
        while (players.Count > 1)
        {
            List<string> nextRound = new List<string>();
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < players.Count; i += 2)
            {
                int index = i; // Capture index for closure
                Thread thread = new Thread(() =>
                {
                    string winner = PlayMatch(players[index], players[index + 1]);
                    lock (lockObj)
                    {
                        nextRound.Add(winner);
                    }
                });

                threads.Add(thread);
                thread.Start();
            }

            // Wait for all matches to complete
            foreach (var thread in threads)
            {
                thread.Join();
            }

            players = nextRound;
            Console.WriteLine("Advancing to next round with players: " + string.Join(", ", players));
        }

        return players[0];
    }

    public static string PlayMatch(string player1, string player2)
    {
        int player1Wins = 0;
        int player2Wins = 0;
        int round = 1;

        while (round <= 3 || player1Wins == player2Wins)
        {
            int move1 = threadRandom.Value!.Next(1, 4);
            int move2 = threadRandom.Value!.Next(1, 4);

            int result = DetermineWinner(move1, move2);

            Console.WriteLine($"Round {round}: {player1} ({MoveToString(move1)}) vs {player2} ({MoveToString(move2)})");

            if (result == 1)
            {
                player1Wins++;
                Console.WriteLine($"{player1} wins round {round}!");
            }
            else if (result == -1)
            {
                player2Wins++;
                Console.WriteLine($"{player2} wins round {round}!");
            }
            else
            {
                Console.WriteLine("It's a tie! Replaying round.");
            }

            if (player1Wins == 2 || player2Wins == 2)
            {
                break;
            }

            round++;
        }

        string matchWinner = player1Wins > player2Wins ? player1 : player2;
        Console.WriteLine($"{matchWinner} wins the match!");
        return matchWinner;
    }

    public static int DetermineWinner(int move1, int move2)
    {
        if (move1 == move2) return 0;
        if ((move1 == ROCK && move2 == SCISSORS) ||
            (move1 == PAPER && move2 == ROCK) ||
            (move1 == SCISSORS && move2 == PAPER))
        {
            return 1;
        }
        return -1;
    }

    private static string MoveToString(int move)
    {
        return move switch
        {
            ROCK => "Rock",
            PAPER => "Paper",
            SCISSORS => "Scissors",
            _ => "Unknown",
        };
    }
}