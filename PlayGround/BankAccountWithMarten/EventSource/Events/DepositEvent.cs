namespace BankAccountWithMarten.EventSource.Events;

public record DepositEvent
{
    public required decimal Amount { get; set; }

    public override string ToString()
    {
        return $"Deposit {Amount} to the account";
    }
}