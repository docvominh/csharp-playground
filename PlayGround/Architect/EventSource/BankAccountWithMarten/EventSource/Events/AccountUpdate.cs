namespace BankAccountWithMarten.EventSource.Events;

public record AccountUpdate
{
    public required string Address { get; init; }

    public override string ToString()
    {
        return $"Account update, new address = {Address}";
    }
}
