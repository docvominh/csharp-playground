namespace BankAccountWithMarten.EventSource.Events;

public class AccountCreate
{
    public required Account Account { get; init; }

    public override string ToString()
    {
        return $"Create Account, Owner = {Account.Owner} Address = {Account.Address}";
    }
}
