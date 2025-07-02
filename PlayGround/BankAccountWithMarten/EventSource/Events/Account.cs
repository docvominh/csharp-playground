namespace BankAccountWithMarten.EventSource.Events;

public class Account
{
    public Guid? Id { get; set; }

    public string? Owner { get; set; }

    public string? Address { get; set; }

    public decimal Balance { get; set; } = 0;
}
