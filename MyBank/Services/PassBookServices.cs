using Banking_System_Application.Models;
using Newtonsoft.Json;

namespace Banking_System_Application.Services;

public class PassBookServices
{
    private readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"AccountData.json");
    private readonly string FilePathPassBook = Path.Combine(Directory.GetCurrentDirectory(), @"PassBookData.json");

    private List<Customers> Accounts = new List<Customers>();
    private List<Passbook> PassBooks = new List<Passbook>();
    private readonly string PassBookPath;

    public PassBookServices(string userName)
    {
        PassBookPath = $"Passbooks/{userName}/{userName}_passbook.txt";
        if (!Directory.Exists($"PassBooks/{userName}"))
        {
            Directory.CreateDirectory($"PassBooks/{userName}"); 
        }
        
        FilePathPassBook = $"Passbooks/{userName}/{userName}_passbook.json";
                
        if(!Directory.Exists($"PassBooks/{userName}"))
        {
            Directory.CreateDirectory($"PassBooks/{userName}");
        }

        LoadDataFromJson();
        LoadDataFromJsonPassBook();

        if (!File.Exists(FilePathPassBook))
        {
            using (StreamWriter writer = new StreamWriter(PassBookPath, true))
            {
                // writer.WriteLine(new string('-', 150));
                // writer.WriteLine("PASSBOOK".PadLeft(67));
                // writer.WriteLine(new string('-', 150));
                // writer.WriteLine("Date              | Transaction Type | Amount | Balance         ");
                // writer.WriteLine(new string('-', 150));

                writer.WriteLine("---------------------------------------------------------------------------");
                writer.WriteLine("                              PASSBOOK                                     ");
                writer.WriteLine("---------------------------------------------------------------------------");
                writer.WriteLine("Date              | Transaction Type   | Amount     | Balance              ");
                writer.WriteLine("---------------------------------------------------------------------------");
            }
        }
    }

    public void PrintPassBookData(string userName)
    {
        var account = Accounts.Find(x => x.Account.UserName == userName);
        if (account != null)
        {
            var data = PassBooks.Find(x => x.AccountId == account.Account.AccountId);
            if (data != null)
            {
                using (StreamWriter writer = new StreamWriter(PassBookPath, true))
                {
                    string date = data.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    writer.WriteLine(
                        $"{date,-18} | {data.TransactionType,-16} | {data.Amount,8:c} | {data.Balance,9:c}");
                }

                Console.Write("Pass Book printed successfully");
            }
            else
            {
                Console.WriteLine("No passbook data found with " + account.Account.AccountId);

            }
        }
        else
        {
            Console.WriteLine("No passbook data found with username: " + userName);
        }
    }
    
    private void SaveDataFromJson()
    {
        var json = JsonConvert.SerializeObject(Accounts, Formatting.Indented);
        File.WriteAllText(FilePath, json);
        Console.WriteLine("Data saved successfully");
    }
        
    private void SaveDataFromJsonPassBook()
    {
        var json = JsonConvert.SerializeObject(PassBooks, Formatting.Indented);
        File.WriteAllText(FilePathPassBook, json);
        Console.WriteLine("Data saved successfully");
    }
    private void LoadDataFromJson()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            Accounts = JsonConvert.DeserializeObject<List<Customers>>(json);
            Console.WriteLine("Data loaded successfully");
        }
    }
        
    private void LoadDataFromJsonPassBook()
    {
        if (File.Exists(FilePathPassBook))
        {
            var json = File.ReadAllText(FilePathPassBook);
            PassBooks = JsonConvert.DeserializeObject<List<Passbook>>(json);
            Console.WriteLine("Data loaded successfully");
        }
    }
}