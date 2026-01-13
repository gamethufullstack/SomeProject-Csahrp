using System.Reflection.PortableExecutable;
class RPSGame
{
    public static void Main()
    {
        Console.WriteLine("GAME ROCK-PAPER-SCISSORS");
        Random rand = new Random();
        string player = "";
        string computer = "";
        bool PlayAgain = true;
        do
        {
            Console.WriteLine(" 1.Enter rock to choose rock");
            Console.WriteLine(" 2.Enter paper to choose paper");
            Console.WriteLine(" 3.Enter scissors to choose scissors");
            Console.WriteLine("4. exit to exit");
            try
            {
                Console.Write("Enter: ");
                player = Console.ReadLine();
                player = player.Trim().ToLower();

                if (player == "exit") 
                    {
                        PlayAgain = false;
                        continue;
                    } 

                if (player != "rock" &&
                    player != "paper" &&
                    player != "scissors")
                {
                    Console.WriteLine("Not valid");
                    continue;
                }
                
                    switch (rand.Next(1, 4))
                    {
                        case 1:
                            computer = "rock";
                            break;
                        case 2:
                            computer = "paper";
                            break;
                        case 3:
                            computer = "scissors";
                            break;
                    }
                    Console.WriteLine("Player: " + player);
                    Console.WriteLine("Computer: " + computer);
                    if (player == "paper" && computer == "rock" ||
                        player == "rock" && computer == "scissors" ||
                        player == "scissors" && computer == "paper")
                    {
                        Console.WriteLine("You won");
                    }

                    else if (player == "rock" && computer == "paper" ||
                        player == "scissors" && computer == "rock" ||
                        player == "paper" && computer == "scissors")
                    {
                        Console.WriteLine("You Lost");
                    }

                    else if (player == computer)
                    {
                        Console.WriteLine("Draw!");
                    }

            }

            catch
            {
                Console.WriteLine("Not valid");
            }

        } while (PlayAgain);
    }
}