namespace BankAccountEventSource.Persistence.Entity;

public class AccountEntity
{
    public required string Id { get; set; }
    public required string Owner { get; set; }
    public string? Address { get; set; }
    public required decimal Balance { get; set; }
    public Guid StreamKey { get; set; }
}
