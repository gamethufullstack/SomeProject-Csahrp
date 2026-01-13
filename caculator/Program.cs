class Caculator
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Welcome to Simple Caculator");
            Console.WriteLine("1. c to calculate");
            Console.WriteLine("2. exit to exit");
            string choice = Console.ReadLine();
            choice = choice.Trim().ToLower();
            if (choice == "c")
            {
                try
                {
                    double result = 0;
                    Console.Write("Enter a: ");
                    double a = double.Parse(Console.ReadLine());
                    Console.Write("Enter b: ");
                    double b = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter a char(+,-,*,/)");
                    char c = char.Parse(Console.ReadLine());

                    switch (c)
                    {
                        case '+':
                            result = a + b;
                            break;
                        case '-':
                            result = a - b;
                            break;
                        case '*':
                            result = a * b;
                            break;
                        case '/':
                            if (b == 0)
                            {
                                Console.WriteLine("Cannot divide by zero");
                                continue;
                            }
                            result = a / b;
                            break;
                        default:
                            Console.WriteLine("not valid");
                            break;
                    }
                    Console.WriteLine("Result: " + result);
                }
                catch
                {
                    Console.WriteLine("Error");
                }
            }

            else if (choice == "exit")
            {
                break;
            }

            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}