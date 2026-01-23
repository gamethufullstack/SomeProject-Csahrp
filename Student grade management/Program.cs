using System;
using System.IO;
using System.Linq;

enum Grade
{
    Excellent,
    Good,
    Average,
    Poor
}
class Student
{
    public string Name { get; set; }
    public double Score { get; set; }
    public Student(string name, double score)
    {
        Name = name;
        Score = score;
    }

    public override string ToString()
    {
        return $"{Name,-10} | {Score,4:F1}";
    }
}

class StudentService
{
    private List<Student> students = new List<Student>();

    public void EnterStudents()
    {
        // Keep asking user to enter students until they type 'q' or empty input
        while (true)
        {
            Console.WriteLine("Enter name(or q to quit)");
            string name = Console.ReadLine();

            //Exit condition
            if (name == "q" || string.IsNullOrWhiteSpace(name))
            {
                break;
            }
            Console.WriteLine("Enter score:");
            string scoreInput = Console.ReadLine();
            if (double.TryParse(scoreInput, out double score))
            {
                if (score < 0 || score > 10)
                {
                    Console.WriteLine("Invalid score\n");
                }
                else
                {
                    students.Add(new Student(name, score));
                    Save();
                    Console.WriteLine("Added successfully!\n");

                }
            }
            else
            {
                Console.WriteLine("Invalid score.Try again\n");
            }
        }
    }

    public double Average()
    {
        // Return -1 to indcate that there is no student data
        if (!students.Any())
        {
            return -1;
        }
        double sum = 0;
        foreach (var s in students)
        {
            sum += s.Score;
        }
        return sum / students.Count;
    }

    public void MaxMin()
    {
        if (!students.Any())
        {
            Console.WriteLine("No data avaible.\n");
            return;
        }

        // Initialize min and max with the first student's score
        // to avoid using extreme values
        double min = students[0].Score;
        double max = students[0].Score;

        foreach (Student s in students)
        {
            if (s.Score < min)
            {
                min = s.Score;
            }
            if (s.Score > max)
            {
                max = s.Score;
            }
        }

        if (min == max)
        {
            Console.WriteLine("\nAll student have the same score");
            foreach (Student s in students)
            {
                Console.WriteLine(s);
            }
        }
        else
        {
            Console.WriteLine("Min score student:");
            foreach (Student s in students)
            {
                if (s.Score == min)
                    Console.WriteLine($"Min:{s}");
            }

            Console.WriteLine("Max score student:");
            foreach (Student s in students)
            {
                if (s.Score == max)
                    Console.WriteLine($"Max:{s}");
            }
        }
    }

    public void Edit()
    {
        if (!students.Any())
        {
            Console.WriteLine("No data avaible.\n");
            return;
        }

        Console.Write("Enter name to edit:");
        string input = Console.ReadLine();

        // Find student by name (case-insensitive)
        Student? student = null;
        foreach(Student s in students)
        {
            if (s.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
            {
                student = s;
                break;
            }
        }

        if (student == null)
        {
            Console.WriteLine("No student found");
            return;
        }

        Console.WriteLine("1.Edit name");
        Console.WriteLine("2.Edit score");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Enter new name: ");
            student.Name = Console.ReadLine();
        }

        else if (choice == "2")
        {
            Console.Write("Enter new score: ");
            string scoreString = Console.ReadLine();
            if (double.TryParse(input, out double newScore)
                && newScore >= 0 && newScore <= 10)
            {
                student.Score = newScore;
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
        Save();
        Console.WriteLine("Updated successfully!");
    }

    public void Delete()
    {
        if (!students.Any())
        {
            Console.WriteLine("No student to delete");
            return;
        }

        Console.WriteLine("Are you sure(yes/no)");
        string choice = Console.ReadLine().Trim().ToLower();

        if (choice == "yes")
        {
            Console.Write("Enter name too delete:");
            string input = Console.ReadLine();

            Student? student = null;

            // Find student by name to delete
            foreach (Student s in students)
            {
                if (s.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    student = s;
                    break;
                }
            }

            if (student == null)
            {
                Console.WriteLine("No student found");
                return;
            }

            students.Remove(student);
            Save();
            Console.WriteLine("Student delete");
        }
    }

    public void SearchByName()
    {
        Console.Write("Enter name to search");
        string input = Console.ReadLine();
        Student student = null;
        foreach(Student s in students)
        {
            if (s.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
            {
                student = s;
                break;
            }
        }
        if (student == null)
        {
            Console.WriteLine("No student found");
            return;
        }
        Console.WriteLine(student);
    }

    public Grade GetGrade(double score)
    {
        if (score >= 9) return Grade.Excellent;
        else if (score >= 6.5) return Grade.Good;
        else if (score >= 4.5) return Grade.Average;
        else return Grade.Poor;
    }

    public void ClassifyStudents()
    {
        if (!students.Any())
        {
            Console.WriteLine("No data avaible.\n");
            return;
        }

        int index = 1;
        Console.WriteLine("List of student scores");
        foreach (Student s in students.OrderByDescending(s => s.Score))
        {
            Grade grade = GetGrade(s.Score);
            Console.WriteLine($"{index}. {s}  -> {grade}");
            index++;
        }

        int index3 = 1;
        Console.WriteLine("List of Top 3 high student scores");
        foreach (Student s in students.OrderByDescending(s => s.Score).Take(3))
        {
            Console.WriteLine($"{index3}. {s}  -> {GetGrade(s.Score)}");
            index3++;
        }
    }

    public void Save()
    {
        using (StreamWriter writer = new StreamWriter("Student.txt"))
        {
            foreach (var s in students)
            {
                writer.WriteLine($"{s.Name}|{s.Score}");
            }
        }
    }

    public void Load()
    {
        if (!File.Exists("Student.txt"))
        {
            return;
        }

        students.Clear();

        var lines = File.ReadAllLines("Student.txt");
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            var parts = line.Split('|');
            string name = parts[0].Trim();
            double score = double.Parse(parts[1].Trim());
            students.Add(new Student(name, score));
        }
    }
}
class Program
{
    static void Menu(StudentService service)
    {
        service.Load();
        Console.WriteLine("===Student grade management program===");
        while (true)
        {
            Console.WriteLine("\n1.enter score list");
            Console.WriteLine("2.Show average");
            Console.WriteLine("3.Show Max/Min score of Student");
            Console.WriteLine("4.Edit Student");
            Console.WriteLine("5.Delete student");
            Console.WriteLine("6.Search student by name");
            Console.WriteLine("7.Clasification of Student");
            Console.WriteLine("8.exit to quit");

            Console.Write("choose option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    service.EnterStudents();
                    break;

                case "2":
                    double avg = service.Average();
                    if (avg >= 0)
                    {
                        Console.WriteLine($"Average: {avg:F2}");
                    }
                    else
                    {
                        Console.WriteLine("No data avaible\n");
                    }
                    break;

                case "3":
                    service.MaxMin();
                    break;

                case "4":
                    service.Edit();
                    break;

                case "5":
                    service.Delete();
                    break;
                case "6":
                    service.SearchByName();
                    break;
                case "7":
                    service.ClassifyStudents();
                    break;
                case "8":
                    Console.WriteLine("GoodBye!");
                    return;
                default:
                    Console.WriteLine("\nInvalid choice");
                    break;
            }
        }
    }
    public static void Main()
    {
        StudentService service = new StudentService();
        service.Load();
        Menu(service);
    }
}
