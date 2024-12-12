public class RockPaperScissors
{
    private const int ROCK = 1;
    private const int PAPER = 2;
    private const int SCISSORS = 3;

    private static int player1Score = 0;
    private static int player2Score = 0;
    private static readonly object lockObj = new object();

    private static readonly ThreadLocal<Random> threadRandom = new ThreadLocal<Random>(() => new Random());

    public static void ResetScores()
    {
        player1Score = 0;
        player2Score = 0;
    }

    public static void UpdateScore(int result)
    {
        if (result == 1) player1Score++;
        else if (result == -1) player2Score++;
    }

    public static bool CheckGameOver(int round = 0)
    {
        if (player1Score == 2 || player2Score == 2) return true;
        if (round >= 3 && player1Score != player2Score) return true;
        return false;
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

    static void Main(string[] args)
    {
        Thread player1 = new Thread(() => Play("Player 1"));
        Thread player2 = new Thread(() => Play("Player 2"));

        player1.Start();
        player2.Start();

        player1.Join();
        player2.Join();

        Console.WriteLine("Game Over!");
    }

    static void Play(string playerName)
    {
        int round = 1;

        while (true)
        {
            int move1 = threadRandom.Value!.Next(1, 4);
            int move2 = threadRandom.Value!.Next(1, 4);

            lock (lockObj)
            {
                Console.WriteLine($"{playerName} plays round {round} with moves {move1} vs {move2}");

                int result = DetermineWinner(move1, move2);

                UpdateScore(result);

                Console.WriteLine($"Scores => Player 1: {player1Score}, Player 2: {player2Score}");

                if (CheckGameOver(round)) return;
            }

            round++;
        }
    }
}