public interface IAccount
{
    void PayInFunds(decimal amount);
    bool WithdrawFunds(decimal amount);
    decimal GetBalance();
    string GetName();
    bool Save(string filename);
    void Save(TextWriter textOut);
    bool SetName(string inName);
}

class CustomerAccount : IAccount
{
    public CustomerAccount(string inName, decimal inBalance)
    {
        name = inName;
        balance = inBalance;
    }

    public CustomerAccount(TextReader textIn)
    {
        name = textIn.ReadLine();
        string balanceText = textIn.ReadLine();
        balance = decimal.Parse(balanceText);
    }

    private string name;
    private decimal balance;

    public void PayInFunds(decimal amount)
    {
        balance += amount;
    }

    public virtual bool WithdrawFunds(decimal amount)
    {
        if (balance < amount) return false;
        balance -= amount;
        return true;
    }

    public decimal GetBalance()
    {
        return balance;
    }

    public string GetName()
    {
        return this.name;
    }

    public static string ValidateName(string name)
    {
        if (name == null)
            return "Name parameter null";
        string trimedName = name.Trim();
        if (trimedName.Length == 0)
        {
            return "No text in the name";
        }
        return "";
    }

    public bool SetName(string inName)
    {
        string reply;
        reply = ValidateName(inName);
        if (reply.Length > 0)
        {
            return false;
        }

        this.name = inName.Trim();
        return true;
    }

    public virtual void Save(TextWriter textOut)
    {
        textOut.WriteLine(name);
        textOut.WriteLine(balance);
    }

    public bool Save(string filename)
    {
        TextWriter textOut = null;
        try
        {
            textOut = new StreamWriter(filename);
            Save(textOut);
        }
        catch
        {
            return false;
        }
        finally
        {
            if (textOut != null)
                textOut.Close();
        }
        return true;
    }

    public static CustomerAccount Load(TextReader textIn)
    {
        CustomerAccount result = null;
        try
        {
            string name = textIn.ReadLine();
            string balanceText = textIn.ReadLine();
            decimal balance = decimal.Parse(balanceText);
            return new CustomerAccount(name, balance);
        }
        catch
        {
            return null;
        }
    }

    public static CustomerAccount Load(string filename)
    {
        CustomerAccount result = null;
        TextReader textIn = null;

        try
        {
            textIn = new StreamReader(filename);
            result = Load(textIn);

        }
        catch
        {
            return null;
        }

        finally
        {
            if (textIn != null) textIn.Close();
        }
        return result;
    }


}

class BabyAccount : CustomerAccount
{
    public BabyAccount(string inName,
        decimal inBalance,
        string inParentName)
        : base(inName, inBalance)
    { parentName = inParentName; }

    private string parentName;

    public string GetParentName()
    {
        return parentName;
    }
    public override bool WithdrawFunds(decimal amount)
    {
        if (amount > 10)
        {
            return false;
        }
        return base.WithdrawFunds(amount);
    }

    public override void Save(TextWriter textOut)
    {
        base.Save(textOut);
        textOut.WriteLine(parentName);
    }

    public BabyAccount(TextReader textIn)
        : base(textIn)
    {
        parentName = textIn.ReadLine();
    }
}
class DictionaryBank
{
    Dictionary<string, IAccount> accountDictionary = new Dictionary<string, IAccount>();
    public IAccount FindAccount(string name)
    {
        if (accountDictionary.ContainsKey(name) == true)
        {
            return accountDictionary[name];
        }

        else
        {
            return null;
        }
    }

    public bool StoreAccount(IAccount account)
    {
        if (accountDictionary.ContainsKey(account.GetName()) == true)
        {
            return false;
        }
        accountDictionary.Add(account.GetName(), account);
        return true;
    }

    public void Save(TextWriter textOut)
    {
        textOut.WriteLine(accountDictionary.Count);
        foreach (IAccount account in accountDictionary.Values)
        {
            textOut.WriteLine(account.GetType().Name);
            account.Save(textOut);
        }
    }

    public bool Save(string filename)
    {
        TextWriter textOut = null;
        try
        {
            textOut = new StreamWriter(filename);
            Save(textOut);
            textOut.Close();
        }
        catch
        {
            return false;
        }
        finally
        {
            if (textOut != null)
                textOut.Close();
        }
        return true;
    }

    public static DictionaryBank Load(TextReader textIn)
    {
        DictionaryBank result = new DictionaryBank();
        string countString = textIn.ReadLine();
        int count = int.Parse(countString);
        for (int i = 0; i < count; i++)
        {
            string className = textIn.ReadLine();
            IAccount account =
                AccountFactory.MakeAccount(className, textIn);
            result.StoreAccount(account);
        }
        return result;
    }

    public static DictionaryBank Load(string filename)
    {
        DictionaryBank result = null;
        TextReader textIn = null;

        try
        {
            textIn = new StreamReader(filename);
            result = Load(textIn);

        }
        catch
        {
            return null;
        }

        finally
        {
            if (textIn != null) textIn.Close();
        }
        return result;
    }
}
class AccountFactory
{
    public static IAccount MakeAccount(string name, TextReader textIn)
    {
        switch (name)
        {
            case "CustomerAccount":
                return new CustomerAccount(textIn);
            case "BabyAccount":
                return new BabyAccount(textIn);
            default:
                return null;
        }
    }
}

public class AccountEditTextUI
{
    public IAccount account;
    public AccountEditTextUI(IAccount inAccount)
    {
        this.account = inAccount;
    }

    public void EditName()
    {
        string newName;
        Console.WriteLine("Name Edit");
        while (true)
        {
            Console.WriteLine("enter new name");
            newName = Console.ReadLine();

            string reply;
            reply = CustomerAccount.ValidateName(newName);

            if (reply.Length == 0)
            {
                break;
            }
            Console.WriteLine("Invalid name: " + reply);
        }
        this.account.SetName(newName);
    }

    public void PayInFunds()
    {
        Console.WriteLine("Enter amount to pay in: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        account.PayInFunds(amount);
    }

    public void WithdrawFunds()
    {
        Console.WriteLine("Enter amount to withdraw: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        if (!account.WithdrawFunds(amount))
        {
            Console.WriteLine("Not enough amount");
        }
    }

    public void Balance()
    {
        Console.WriteLine("Balance: " + account.GetBalance());
    }

    public void DoEdit()
    {
        string command;
        do
        {
            Console.WriteLine("\nEditing account for {0}", account.GetName());
            Console.WriteLine(" Enter name to edit name");
            Console.WriteLine(" Enter pay to pay in funds");
            Console.WriteLine(" Enter draw to draw out funds");
            Console.WriteLine(" Enter balance to show balance");
            Console.WriteLine(" Enter exit to exit program");
            Console.Write("Enter command : ");
            command = Console.ReadLine();
            command = command.Trim();
            command = command.ToLower();
            switch (command)
            {
                case "name":
                    EditName();
                    break;
                case "pay":
                    PayInFunds();
                    break;
                case "draw":
                    WithdrawFunds();
                    break;
                case "balance":
                    Balance();
                    break;
                default:
                    Console.WriteLine("Please enter correct");
                    break;

            }
        } while (command != "exit");
    }
}
class BankProgram
{


    public static void SoundSiren()
    {
        Console.WriteLine("Insert Loud Noise Here");
    }

    public static void Main()
    {

        string newName;
        DictionaryBank ourBank = new DictionaryBank();

        CustomerAccount newAccount = new CustomerAccount("Dat", 10000);
        if (ourBank.StoreAccount(newAccount) == true)
        {
            Console.WriteLine("CustomerAccount added to bank");
        }

        BabyAccount babyAccount = new BabyAccount("DatBaby", 1000, "Dat's Parent");
        if (ourBank.StoreAccount(babyAccount) == true)
        {
            Console.WriteLine("BabyAccount added to bank");
        }

        ourBank.Save("Test.txt");
        DictionaryBank loadbank = DictionaryBank.Load("Test.txt");

        IAccount storedAccount = loadbank.FindAccount("Dat");
        if (storedAccount != null)
        {
            Console.WriteLine("CustomerAccount found in the bank");
        }

        storedAccount = loadbank.FindAccount("DatBaby");
        if (storedAccount != null)
        {
            Console.WriteLine("BabyAccount found in the bank");
        }

        int errorcount = 0;
        string reply;

        reply = CustomerAccount.ValidateName(null);
        if (reply != "Name parameter null")
        {
            Console.WriteLine("Null name test failed");
            errorcount++;
        }

        reply = CustomerAccount.ValidateName(" ");
        if (reply != "No text in the name")
        {
            Console.WriteLine("Blank string name test failed");
            errorcount++;
        }

        CustomerAccount a = new CustomerAccount("Dat", 50);
        if (!a.SetName("Jim"))
        {
            Console.WriteLine("Jim GetName failed");
            errorcount++;
        }

        if (a.GetName() != "Jim")
        {
            Console.WriteLine("Jim GetName failed");
            errorcount++;
        }

        if (!a.SetName(" Pete "))
        {
            Console.WriteLine("Pete trim SetName failed");
            errorcount++;
        }

        if (a.GetName() != "Pete")
        {
            Console.WriteLine("Pete GetName failed");
            errorcount++;
        }

        if (errorcount > 0)
        {
            SoundSiren();
        }
        AccountEditTextUI edit = new AccountEditTextUI(a);
        edit.DoEdit();
    }
}