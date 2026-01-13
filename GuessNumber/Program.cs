class GuessNumber
{
   public static void Main()
    {
        Random rand = new Random();
        int computerandom = rand.Next(1, 101);
        int playernumber = 0;
        Console.WriteLine("Welcome to the Guess number game");

        while (computerandom != playernumber)
        {
            Console.Write("Enter a number (1-100) too guess:");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out playernumber))
            {
                Console.WriteLine("This is not a number");
                continue;
            }

            if ((playernumber < 0) || (playernumber > 100))
            {
                Console.WriteLine("Please enter number between 1 and 100!");
            }

            else if (playernumber < computerandom)
            {
                Console.WriteLine("To low!");
            }
            else if (playernumber > computerandom)
            {
                Console.WriteLine("To high!");
            }
            else
            {
                Console.WriteLine("Congratulation! you guessed the number.");
            }
        }
    }
}