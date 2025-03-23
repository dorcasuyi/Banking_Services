// See https://aka.ms/new-console-template for more information
using System;
using Banking_System_Application.Models;
using Banking_System_Application.Services;

namespace Banking_System_Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AccountServices accountServices = new AccountServices();
            // BankServices bankServices = new BankServices(); 
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("----Welcome To My Bank----");
            
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("----Menu----");
        
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("1. ");
                Console.ForegroundColor = ConsoleColor.White; 
                Console.WriteLine("Register");
        
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("2. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Login");
        
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("3. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Exit");
        
        
                Console.WriteLine("Enter your choice: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Register(accountServices);
                        break;
                    case "2":
                        string username;
                        if (Login(accountServices, out username))
                        {
                            Console.WriteLine("Login Successfully!");
                            Console.WriteLine("Welcome to My Bank");

                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("----Menu----");
        
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("1. ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Deposit");
        
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("2. ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Withdraw");
        
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("3. ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Print Passbook");
                                
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("4. ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Check Balance");
                                
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("5. ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Check Balance");
                                
                                Console.WriteLine("Exit ");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        BankServices bankServices = new BankServices(username);
                                        Deposit(bankServices, username);
                                        break;
                                        
                                    case "2":
                                        BankServices bankServices2 = new BankServices(username);
                                        Withdraw(bankServices2, username);
                                        break;
                                    case "3":
                                        PassBookServices passBookServices = new PassBookServices(username);
                                        PrintPassBook(passBookServices, username);
                                        break;
                                    case "4":
                                        BankServices bankServices3 = new BankServices(username);
                                        CheckBalance(bankServices3, username);
                                        break;
                                    case "5":
                                        Console.Write("Good Bye!");
                                        return;
                                    default:
                                        Console.WriteLine("Invalid choice, please try again");
                                        break;
                                }
                            }
                        };
                        break;
                    case "3":
                        Console.WriteLine("GoodBye");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please select again");
                        break;
                }
            }
        }

        static void Register(AccountServices accountServices)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n----Register New Customer----");
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter your first name: ");
            string firstName = Console.ReadLine();
            
            Console.Write("\nEnter your second name: ");
            string secondName = Console.ReadLine();
            
            Console.Write("\nEnter your Date Of Birth (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.Write("\nEnter the Address: ");
                string address = Console.ReadLine();
                
                Console.Write("\nEnter the phone number: ");
                string phoneNumber = Console.ReadLine();
                
                Console.Write("\nEnter the Gender(Male/Female/Other): ");
                string gender = Console.ReadLine();
                
                Console.Write("\nEnter the account type(Savings/Current): ");
                string acctType = Console.ReadLine();
                accountServices.CreateCustomer(firstName, secondName, date, address, phoneNumber,gender, acctType);
                
            }
            else
            {
                Console.WriteLine("\nInvalid Date Format, please try again");
            }
        }

        static bool Login(AccountServices accountServices, out string user)
        {
            user = "0";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n----Login----");
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter your username: ");
            String userName = Console.ReadLine();
            user = userName;
            
            Console.Write("\nEnter your password: ");
            String password = Console.ReadLine();
            
            return accountServices.GetCustomer(userName, password);
        }

        static void Deposit(BankServices bankServices, string userName)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("-------Deposits------");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter your amount : ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                bankServices.DepositAmount(userName, amount);
            }
            else
            {
                Console.WriteLine("Invalid amount or password, please try again");
            }
        }

        static void Withdraw(BankServices bankServices, string userName)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("-------Withdraw------");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter your amount : ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                bankServices.DepositAmount(userName, amount);
            }
            else
            {
                Console.WriteLine("Invalid amount or password, please try again");
            }
        }

        static void CheckBalance(BankServices bankServices, string userName)
        {
            if (userName != null)
            {
                bankServices.CheckBalance(userName);
            }
            else
            {
                Console.WriteLine("Invalid name, please try again");
            }
        }

        static void PrintPassBook(PassBookServices passBookServices, string userName)
        {
            if (userName != null)
            {
                passBookServices.PrintPassBookData(userName);
            }
            else
            {
                Console.WriteLine("Invalid name, please try again");
            }
        }
    }
}


