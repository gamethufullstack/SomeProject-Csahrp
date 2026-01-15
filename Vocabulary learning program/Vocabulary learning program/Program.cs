class VocabularyLearningProgram
{
    public static void Main()
    {
        VocabularyManager manager = new VocabularyManager();
        manager.LoadFromFile();
        
        while (true)
        {
            Console.WriteLine("1.Add word");
            Console.WriteLine("2.show all words");
            Console.WriteLine("3.Edit word or meaning");
            Console.WriteLine("4.review(random)");
            Console.WriteLine("5.exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.AddWord();
                    break;

                case "2":
                    manager.ShowWord();
                    break;

                case "3":
                    manager.Edit();
                    break;

                case "4":
                    manager.Review();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}
