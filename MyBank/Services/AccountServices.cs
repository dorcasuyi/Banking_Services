using System.Data;
using System.Security.Principal;
using Banking_System_Application.Models;
using Newtonsoft.Json;

namespace Banking_System_Application.Services;

public class AccountServices
{
    private readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"AccountData.json");
    
    private List<Customers> customers = new List<Customers>();

    private int nextIdForCustomer = 1;
    private int nextIdForAccount = 1;
    
    public AccountServices()
    {
        LoadDataFromJson();
        
    }
    

    public void CreateCustomer(string firstName, string lastName,DateTime dob, string address, string phoneNumber, string gender,
        string accountType)
    {
        var newCustomer = new Customers
        {
            CustomerId = nextIdForCustomer++,
            FirstName = firstName,
            LastName = lastName,
            DOB = dob,
            Address = address,
            PhoneNumber = phoneNumber,
            Gender = gender,
            AccountType = accountType
        };
        Console.WriteLine($"Wait, account details are generating in process........");
        newCustomer.Account = GenerateAccountDetails(accountType, firstName);
        customers.Add(newCustomer);
        Console.WriteLine("Registered Successfully");
        SaveDataFromJson();
    }

    public bool GetCustomer(string userName, string password)
    {
        var customer = customers.FirstOrDefault(x => x.Account.UserName == userName && x.Account.Password == password);
        if (customer != null)
        {
            return true;
        }
        else
        {
            Console.WriteLine("Account details not found, please try again.");
            return false;
        }  
    }

    private Account GenerateAccountDetails(string accType, string firstName)
    {
        var accountDetails = new Account
        {
            AccountId = nextIdForAccount++,
            Balance = GetMinimumBalance(accType),
            UserName = GenerateUserName(firstName),
            Password = GeneratePassword(),
            AccountNumber = GenerateAccountNumber(),
        };
        return accountDetails;
    }

    private string GenerateAccountNumber()
    {
        return "HDFC" + "1001" + new Random().Next(10000, 99999);
    }
    private string GeneratePassword()
    {
        return  "pass" + "0" + new Random().Next(10000, 99999);
    }
    private string GenerateUserName(string name)
    {
        return name.ToLower() + "hdfc" + new Random().Next(10000, 99999);
    }
    private decimal GetMinimumBalance(string accType)
    {
        return accType.ToLower() == "savings" ? 1000 : 5000;
    }

    private void SaveDataFromJson()
    {
        var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
        File.WriteAllText(FilePath, json);
        Console.WriteLine("Data saved successfully");
    }
    private void LoadDataFromJson()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            customers = JsonConvert.DeserializeObject<List<Customers>>(json);
            //Togenerate the id after the id available in json file
            if (customers.Count != null)
            {
                nextIdForCustomer = customers[customers.Count - 1].CustomerId + 1;
            }
            Console.WriteLine("Data loaded successfully");
        }
    }
}