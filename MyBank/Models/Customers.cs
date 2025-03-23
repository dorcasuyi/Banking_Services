namespace Banking_System_Application.Models;

public class Customers
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string AccountType { get; set; }
    public string Gender { get; set; }
    public DateTime DOB { get; set; }
    public Account Account { get; set; }
}