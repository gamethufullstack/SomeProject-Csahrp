class Calculator
{
    public static void Main()
    { double result = 0;
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
                    Console.WriteLine("Please choose simple caculate or Advance(s/a))");
                    string input = Console.ReadLine();
                    if (input.Trim().ToLower() == "s")
                    {
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

                    else if (input.Trim().ToLower() == "a")
                    {
                        Console.WriteLine("Enter number");
                       double number = double.Parse(Console.ReadLine());
                        Console.WriteLine("Enter a ^,√");
                        char c = char.Parse(Console.ReadLine());

                        switch (c)
                        {
                            case '^':
                                Console.Write("Enter Exponent:");
                                int exponent = int.Parse(Console.ReadLine());
                                result = Math.Pow(number, exponent);
                                break;
                            case '√':
                                result = Math.Sqrt(number);
                                break;
                        }
                        Console.WriteLine("Result:" +  result);
                    }
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
