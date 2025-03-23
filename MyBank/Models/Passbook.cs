namespace Banking_System_Application.Models;

public class Passbook
{
    public int PassbookID { get; set; }
    public int AccountId { get; set; }
    public DateTime Date { get; set; }
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
}