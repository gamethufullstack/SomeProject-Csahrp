enum Choice
{
    Rock,
    Paper,
    Scissors,
    Exit
}
class RPSGame
{
    static void ShowMenu()
    {
        Console.WriteLine(" 1.Enter rock to choose rock");
        Console.WriteLine(" 2.Enter paper to choose paper");
        Console.WriteLine(" 3.Enter scissors to choose scissors");
        Console.WriteLine("4. exit to exit");
    }
    static Choice GetPlayerchoice()
    {
        while (true)
        {
 
            string input = Console.ReadLine().Trim().ToLower();
            switch (input)
            {
                case "rock":
                    return Choice.Rock;
                case "paper":
                    return Choice.Paper;
                case "scissors":
                    return Choice.Scissors;
                case "exit":
                    return Choice.Exit;
                default:
                    Console.WriteLine("Not valid, try again");
                    break;
            }

        }
    }

    static Choice GetcomputerChoice(Random rand)
    {
        int value = rand.Next(0, 3);
        return (Choice)value;
    }

    static string GetResult(Choice player, Choice computer)
    {
        if (player == computer)
        {
            return "Draw";
        }

        if (player == Choice.Paper && computer == Choice.Rock ||
            player == Choice.Rock && computer == Choice.Scissors ||
            player == Choice.Scissors && computer == Choice.Paper)
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
            ShowMenu();
            Choice player = GetPlayerchoice();
            if (player == Choice.Exit)
            {
                break;
            }

            Choice computer = GetcomputerChoice(rand);
            Console.WriteLine($"Player: {player}");
            Console.WriteLine($"Computer:{computer}");
            string result = GetResult(player, computer);
            Console.WriteLine(result);
        }
    }
}
