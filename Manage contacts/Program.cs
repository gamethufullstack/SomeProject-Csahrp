class ManagerConntactProgram
{
    public static void Main()
    {
        Manager manager = new Manager();
        manager.Load();

        while (true)
        {
            Console.WriteLine("1.Add Contact");
            Console.WriteLine("2.Show all Contact");
            Console.WriteLine("3.Edit Contact");
            Console.WriteLine("4.Delete Contact");
            Console.WriteLine("5.Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.Add();
                    break;
                case "2":
                    manager.Show();
                    break;
                case "3":
                    manager.Edit();
                    break;
                case "4":
                    manager.Delete();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Please enter correct option");
                    break;
            }
        }
    }
}