namespace BankAccountWithMarten.Models;

public class AccountModel
{
    public Guid? Id { get; set; }
    public required string Owner { get; set; }
    public string? Address { get; set; }
    public required decimal Balance { get; set; }
}