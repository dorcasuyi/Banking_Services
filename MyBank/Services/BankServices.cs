using Banking_System_Application.Models;
using Newtonsoft.Json;
using System;

namespace Banking_System_Application.Services

{
    public class BankServices
    {
        private readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"AccountData.json");
        private string FilePathPassBook;
    
        private List<Customers> Accounts = new List<Customers>();
        private List<Passbook> PassBooks = new List<Passbook>();

        private int newId = 1;

        public BankServices(string userName)
        {
            FilePathPassBook = $"Passbooks/{userName}/{userName}_passbook.json";
                
            if(!Directory.Exists($"PassBooks/{userName}"))
            {
                Directory.CreateDirectory($"PassBooks/{userName}");
            }
            LoadDataFromJson();
            LoadDataFromJsonPassBook();
        }

        public void DepositAmount(string userName, decimal amount)
        {
            var acc = Accounts.FirstOrDefault(x => x.Account.UserName == userName);
            if (acc != null)
            {
                acc.Account.Balance += amount;
                var bal = acc.Account.Balance;
                
                // FilePathPassBook = $"Passbooks/{acc.Account.AccountId}/{acc.Account.UserName}_passbook.json";
                //
                // if(!Directory.Exists($"PassBooks/{acc.Account.UserName}"))
                // {
                //     Directory.CreateDirectory($"PassBooks/{acc.Account.UserName}");
                // }
                Directory.CreateDirectory($"Passbooks/{acc.Account.UserName}");
                var passbookData = new Passbook
                {
                    PassbookID = newId++,
                    AccountId = acc.Account.AccountId,
                    Date = DateTime.Now,
                    TransactionType = "Deposit",
                    Amount = amount,
                    Balance = bal,
                };
                PassBooks.Add(passbookData);
                Console.WriteLine("Amount Deposited successfully");
                SaveDataFromJson();
                SaveDataFromJsonPassBook();
            }
            else
            {
                Console.WriteLine("No data found with username: " + userName);
            }
        }

        public void WithdrawAmount(string userName, decimal amount)
        {
            var acc = Accounts.FirstOrDefault(x => x.Account.UserName == userName);
            if (acc != null)
            {
                acc.Account.Balance -= amount;
                // var bal = acc.Account.Balance;

                var passbookData = new Passbook
                {
                    PassbookID = newId++,
                    AccountId = acc.Account.AccountId,
                    Date = DateTime.Now,
                    TransactionType = "Withdraw",
                    Amount = amount,
                    Balance = acc.Account.Balance,
                };
                PassBooks.Add(passbookData);
                Console.WriteLine("Amount Withdrawn successfully");
                // Console.WriteLine("Amount Withdrawn successfully");
                SaveDataFromJson();
                SaveDataFromJsonPassBook();
            }
            else
            {
                Console.WriteLine("No data found with username: " + userName);
            }
        }

        public void CheckBalance(string userName)
        {
            var account = Accounts.Find(x=>x.Account.UserName == userName);
            if (account != null)
            {
                Console.WriteLine($"Available balance is: {account.Account.Balance}");
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
                //Togenerate the id after the id available in json file
                if (PassBooks.Count != null)
                {
                    newId = PassBooks[PassBooks.Count - 1].PassbookID + 1;
                }
                Console.WriteLine("Data loaded successfully");
            }
        }
        
        
    }
}
