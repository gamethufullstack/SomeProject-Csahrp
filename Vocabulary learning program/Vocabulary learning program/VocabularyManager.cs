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

    public void SaveToFile()
    {
        using(StreamWriter writer = new StreamWriter("words.txt", true))
        {
           Word w = words[words.Count - 1];
           writer.WriteLine($"{w.Text}|{w.Meaning}");
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