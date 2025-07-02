using BankAccountWithMarten.EventSource.Events;

namespace BankAccountWithMarten.EventSource;

public class AccountAggregate
{
    public Guid Id { get; set; }

    public Account Account { get; set; } = new ();

    public void Apply(AccountCreate @event)
    {
        Account = @event.Account;
    }

    public void Apply(AccountUpdate @event)
    {
        Account.Address = @event.Address;
    }

    public void Apply(DepositEvent @event) => Account.Balance += @event.Amount;

    public void Apply(WithdrawEvent @event) => Account.Balance -= @event.Amount;
}
