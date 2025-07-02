using BankAccountWithMarten.EventSource.Events;
using Marten.Events.Aggregation;

namespace BankAccountWithMarten.EventSource;

public class AccountHistory
{
    public Guid Id { get; set; }

    public List<string> History = [];
}

public class AccountHistoryProjection : SingleStreamProjection<AccountHistory>
{
    public AccountHistory Create(AccountCreate @event)
    {
        AccountHistory accountHistory = new ();

        Apply(@event, accountHistory);

        return accountHistory;
    }

    public void Apply(AccountCreate @event, AccountHistory accountHistory)
    {
        accountHistory.History.Add(@event.ToString());
    }

    public void Apply(AccountUpdate @event, AccountHistory accountHistory)
    {
        accountHistory.History.Add(@event.ToString());
    }

    public void Apply(DepositEvent @event, AccountHistory accountHistory) =>
        accountHistory.History.Add(@event.ToString());

    public void Apply(WithdrawEvent @event, AccountHistory accountHistory) =>
        accountHistory.History.Add(@event.ToString());
}
