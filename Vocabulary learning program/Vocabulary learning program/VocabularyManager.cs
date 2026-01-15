class VocabularyManager
{
    List<Word> words = new List<Word>();
    private Random random = new Random();
    public void AddWord()
    {
        Console.Write("Enter a word: ");
        string text = Console.ReadLine();
        Console.Write("Enter meaning: ");
        string meaning = Console.ReadLine();
        words.Add(new Word { Text = text, Meaning = meaning });
        SaveToFile();
        Console.WriteLine("Word added!");
    }

    public void ShowWord()
    {
        if (words.Count == 0)
        {
            Console.WriteLine("It's empty");
            return;
        }

        foreach(var w in words)
        {
            Console.WriteLine($"{w.Text} - {w.Meaning}");
        }
    }

    public void Review()
    {
        if (words.Count == 0)
        {
            Console.WriteLine("no word to review");
            return;
        }
        int index = random.Next(words.Count);
        Word w = words[index];
        Console.WriteLine($"What is the meaning {w.Text}?");
        string answer = Console.ReadLine();
        
        if (answer.Equals(w.Meaning, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You are right!");
        }
        else
        {
            Console.WriteLine("You are wrong!");
        }
    }

    public void Edit()
    {
        if (words.Count == 0)
        {
            Console.WriteLine("No word to edit");
            return;
        }

        Console.Write("Enter word to edit:");
        string input = Console.ReadLine();

        /* Loop through words, find matching Text (ignore case), else w = null*/
        Word w = words.FirstOrDefault(
            x => x.Text.Equals(input, StringComparison.OrdinalIgnoreCase));
        if (w == null)
        {
            Console.WriteLine("Word not found!");
            return;
        }

        Console.WriteLine("1.Edit word");
        Console.WriteLine("2.Edit meaning:");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Enter new word:");
            w.Text = Console.ReadLine();
        }

        else if (choice == "2")
        {
            Console.Write("Enter new meaning:");
            w.Meaning = Console.ReadLine();
        }

        else
        {
            Console.WriteLine("Invalid choice");
        }

        SaveToFile();
        Console.WriteLine("Updated successfully!");
    }

    public void SaveToFile()
    {
        using(StreamWriter writer = new StreamWriter("words.txt"))
        {
            foreach(var w in words)
            {
                writer.WriteLine($"{w.Text}|{w.Meaning}");
            }
        }
    }
    public void LoadFromFile()
    {
        if (!File.Exists("words.txt"))
        {
            return;
        }
        words.Clear();
        string[] lines = File.ReadAllLines("words.txt");
        foreach(string line in lines)
        {
            string[] parts = line.Split('|');
            {
                words.Add(new Word
                {
                    Text = parts[0],
                    Meaning = parts[1]
                });
            }
        }
    }
}
