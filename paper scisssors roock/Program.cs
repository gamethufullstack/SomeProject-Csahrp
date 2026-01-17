class RPSGame
{
    static string GetPlayerchoice()
    {
        while (true)
        {
            Console.WriteLine(" 1.Enter rock to choose rock");
            Console.WriteLine(" 2.Enter paper to choose paper");
            Console.WriteLine(" 3.Enter scissors to choose scissors");
            Console.WriteLine("4. exit to exit");

            string input = Console.ReadLine().Trim().ToLower();

            if (input == "exit")
            {
                return "exit";
            }

            if (input == "rock" || input == "paper" || input == "scissors")
                return input;
            Console.WriteLine("Not valid, try again");
        }
    }

    static string GetcomputerChoice(Random rand)
    {
        int value = rand.Next(1, 4);
        switch (value)
        {
            case 1:return "rock";
            case 2:return "paper";
            case 3:return "scissors";
            default:return "rock";
        }
    }

    static string GetResult(string player, string computer)
    {
        if (player == computer)
        {
            return "Draw";
        }

        if (player == "paper" && computer == "rock" ||
            player == "rock" && computer == "scissors" ||
            player == "scissors" && computer == "paper")
        {
            return "You win!";
        }
        return "You lose";
    }

    public static void Main()
    {
        Console.WriteLine("GAME ROCK-PAPER-SCISSORS");
        Random rand = new Random();
        bool PlayAgain = true;

        while (PlayAgain)
        {
            string player = GetPlayerchoice();
            if (player == "exit")
            {
                break;
            }

            string computer = GetcomputerChoice(rand);
            Console.WriteLine($"Player: {player}");
            Console.WriteLine($"Computer:{computer}");
            string result = GetResult(player, computer);
            Console.WriteLine(result);
        }
    }
}
