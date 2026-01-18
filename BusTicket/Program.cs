enum TypeTicket
{
    Student  = 0,
    Normal = 1,
    Vip = 2
}

class Ticket
{
    public int Code { get; }
    public string Name { get; }
    public int Price { get; }

    public Ticket(int code, string name, int price)
    {
        Code = code;
        Name = name;
        Price = price;
    }
}

class VendingMachine
{
    private int currentMoney;
    List<Ticket> tickets;


    public VendingMachine()
    {
        tickets = new List<Ticket>()
        {
            new Ticket(1, "Student", 5000),
            new Ticket(2, "Normal", 10000),
            new Ticket(3, "Vip", 20000),
        };
    }

    public IReadOnlyList<Ticket> GetList()
    {
        return tickets;
    }
    public bool InsertMoney(int amount)
    {
        if (amount != 5000 && amount != 10000 && amount != 20000)
        {
            return false;
        }
        currentMoney += amount;
        return true;
    }

    public bool BuyTicket(TypeTicket type)
    {
        Ticket ticketselect = tickets[(int)type];

        if (currentMoney < ticketselect.Price)
        {
            return false;
        }

        currentMoney -= ticketselect.Price;
        return true;
    }

    public int ReturnMoney()
    {
        int change = currentMoney;
        currentMoney = 0;
        return change;
    }

    public int CurrentMoney()
    {
        return currentMoney;
    }
}

class Program
{
    public static void Main()
    {
        VendingMachine ticket = new VendingMachine();
        Console.WriteLine("===Bus ticket program===");

        while (true)
        {
            Console.WriteLine("1.View the list bus ticket");
            Console.WriteLine("2.Insert money");
            Console.WriteLine("3.Buy ticket");
            Console.WriteLine("4.Return money");
            Console.WriteLine("5.Exit");

            string choice = Console.ReadLine().Trim().ToLower();

            switch (choice)
            {
                case "1":
                    foreach (var t in ticket.GetList())
                    {
                        Console.WriteLine($"{t.Code}, {t.Name} - {t.Price}");
                    }
                    break;

                case "2":
                    Console.Write("Enter money(5000/10000/20000):");
                    if (int.TryParse(Console.ReadLine(), out int money))
                    {
                        if (ticket.InsertMoney(money))
                        {
                            Console.WriteLine("Insert money sucess!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid money");
                        }
                    }
                    break;

                case "3":
                    Console.Write("Enter your age: ");
                    int.TryParse(Console.ReadLine(), out int age);

                    Console.Write("Enter ticket (student/normal/vip):");
                    string input = Console.ReadLine().Trim().ToLower();

                    if (Enum.TryParse<TypeTicket>(input, true, out var typeticket))
                    {
                        if (age > 18 && typeticket == TypeTicket.Student)
                        {
                            Console.WriteLine("Over 18 years old cannot buy this ticket");
                        }
                        else
                        {
                            if (ticket.BuyTicket(typeticket))
                            {
                            Console.WriteLine($"Ticket: {typeticket} purchase successfully!");
                            }

                            else
                            {
                                Console.WriteLine("Not enough money");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid type ticket");
                    }
                    break;

                case "4":
                    int change = ticket.ReturnMoney();
                    Console.WriteLine($"Returned: {change} VND");
                    break;

                case "5":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            Console.WriteLine($"Current money: {ticket.CurrentMoney()} VND");
        }
    }
}
