using System.ComponentModel.DataAnnotations;

public class Manager
{
    List<Contact> contacts = new List<Contact>();

    public void Add()
    {
        Console.Write("enter name: ");
        string name = Console.ReadLine();
        Console.Write("enter phonenumber: ");
        string phonenumber = Console.ReadLine();
        contacts.Add(new Contact { Name = name, Phonenumber = phonenumber });
        Save();
        Console.WriteLine("Added!");
    }

    public void Show()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("It's empty");
            return;
        }
        foreach(var c in contacts)
        {
            Console.WriteLine($"{c.Name}-{c.Phonenumber}");
        }
    }

    public void Edit()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contact to edit");
            return;
        }
        Console.Write("Enter name to edit:");
        string input = Console.ReadLine();

        Contact c = null;
        foreach(Contact x in contacts)
        {
            if (x.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
            {
                c = x;
                break;
            }
        }

        if (c == null)
        {
            Console.WriteLine("Contact not found");
            return;
        }

        Console.WriteLine("1.Edit name");
        Console.WriteLine("2.Edit phone number");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Enter new name:");
            c.Name = Console.ReadLine();
        }
        else if(choice == "2")
        {
            Console.Write("Enter new phone number:");
            c.Phonenumber = Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Invalid choice");
        }
        Save();
        Console.WriteLine("Updated successfully!");
    }

    public void Delete()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contact to delete");
            return;
        }
        Console.WriteLine("Are you sure?(yes/no)");
        string choice = Console.ReadLine();

        if (choice.Trim().ToLower() == "yes")
        {
            Console.Write("Enter name to delete:");
            string input = Console.ReadLine();

            Contact c = null;
            foreach (Contact x in contacts)
            {
                if (x.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    c = x;
                    break;
                }
            }

            if (c == null)
            {
                Console.WriteLine("Contact not found");
                return;
            }
            contacts.Remove(c);
            Save();
            Console.WriteLine("Contact delete");
        }

        else if (choice.Trim().ToLower() == "no")
        {
            Console.WriteLine("OK!");
            return;
        }
        else
        {
            return;
        }
    }

    public void Save()
    {
        using(StreamWriter writer = new StreamWriter("contact.txt"))
        {
            foreach(var c in contacts)
            {
                writer.WriteLine($"{c.Name}-{c.Phonenumber}");
            }
        }
    } 

    public void Load()
    {
        if (!File.Exists("contact.txt"))
        {
            return;
        }

        contacts.Clear();

        string[] lines = File.ReadAllLines("contact.txt");
        foreach (string line in lines)
        {
            string[] parts = line.Split('-');
            if (parts.Length == 2)
            {
                contacts.Add(new Contact
                {
                    Name = parts[0],
                    Phonenumber = parts[1]
                });
            }
        }
    }
}