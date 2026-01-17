using System;

class Calculator
{
    public static  double SimpleCaculate()
    {
        double result = 0;
        Console.Write("Enter a: ");
        if(!double.TryParse(Console.ReadLine(), out double a))
        {
            Console.WriteLine("Invalid number");
            return 0;
        }

        Console.Write("Enter b: ");
        double.TryParse(Console.ReadLine(), out double b);

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
                    break;
                }
                result = a / b;
                break;

            default:
                Console.WriteLine("not valid");
                break;
        }

        return result;
    }

    public static double AdvanceCaculate()
    {
        double result = 0;
        Console.WriteLine("Enter number");
        if (!double.TryParse(Console.ReadLine(), out double number))
        {
            Console.WriteLine("Invalid number");
            return 0;
        }

        Console.WriteLine("Enter ^ or √");
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

            default:
                Console.WriteLine("Invalid operator");
                break;
        }
        return result;
    }
    public static void Main()
    {
        double result = 0;
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
                        SimpleCaculate();
                    }

                    else if (input.Trim().ToLower() == "a")
                    {
                        AdvanceCaculate();
                    }

                    Console.WriteLine("Result: " + result);
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
