enum DrinkType
{
    Coca,
    Pepsi,
    Sting,
    Water
}

class Drink
{
    public int Id { get; }
    public string Name { get; }
    public int Price { get; }

    public Drink(int Id, string Name, int Price)
    {
        this.Id = Id;
        this.Name = Name;
        this.Price = Price;
    }
}

class VendingMachine
{
    private int currrentMoney;
    List<Drink> drinks;

    public VendingMachine()
    {
        drinks = new List<Drink>
        {
            new Drink(1, "Coca", 10000),
            new Drink(2, "Pepsi", 9000),
            new Drink(3, "Sting", 8000),
            new Drink(4, "Water", 5000),
        };
    }

    public List<Drink> GetDrinks()
    {
        return drinks;
    }

    public bool InsertMoney(int amount)
    {
        if (amount != 5000 && amount != 10000 && amount != 20000)
        {
            return false;
        }
        currrentMoney += amount;
        return true;
    }

    public bool BuyDrink(DrinkType type)
    {
        Drink? selectdrink = null;

        foreach(var drink in drinks)
        {
            if (drink.Name.Equals(type.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                selectdrink = drink;
                break;
            }
        }

        if (selectdrink == null)
        {
            return false;
        }

        if (currrentMoney < selectdrink.Price)
        {
            return false;
        }

        currrentMoney -= selectdrink.Price;
        return true;
    }

    public int ReturnExcessMoney()
    {
        int change = currrentMoney;
        currrentMoney = 0;
        return change;
    }

    public int GetCurrentMoney()
    {
        return currrentMoney;
    }
}

class Program
{
    public static void Main()
    {
        VendingMachine machine = new VendingMachine();
        Console.WriteLine("===Vending Machine===");

        while (true)
        {
            Console.WriteLine(" 1.View list of drinks");
            Console.WriteLine(" 2.Insert money");
            Console.WriteLine(" 3.Buy drinks");
            Console.WriteLine(" 4.Return change");
            Console.WriteLine("Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    foreach(var d in machine.GetDrinks())
                    {
                        Console.WriteLine($"{d.Id}, {d.Name}-{d.Price}");
                    }
                    break;
                case "2":
                    Console.Write("Enter money(5000/10000/20000): ");
                    if (int.TryParse(Console.ReadLine(), out int money))
                    {
                        if (machine.InsertMoney(money))
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
                    Console.Write("choose drink (coca/pepsi/sting/water):");
                    string input = Console.ReadLine().Trim().ToLower();

                    if (Enum.TryParse<DrinkType>(input, true, out var drinkType))
                    {
                        if (machine.BuyDrink(drinkType))
                        {
                            Console.WriteLine("Buy sucess!");
                        }
                        else
                        {
                            Console.WriteLine("Not enough money");
                        }
                    }

                    else
                    {
                        Console.WriteLine("Invalid money");
                    }
                    break;

                case "4":
                    int change = machine.ReturnExcessMoney();
                    Console.WriteLine($"Returned: {change} VND");
                    break;
                case "5":
                    Console.WriteLine("Goodbye");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            Console.WriteLine($"Current money: {machine.GetCurrentMoney()}");
        }
    }
}