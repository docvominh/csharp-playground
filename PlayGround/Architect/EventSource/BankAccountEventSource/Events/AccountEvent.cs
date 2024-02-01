using BankAccountEventSource.Controller;
using BankAccountEventSource.Models;

namespace BankAccountEventSource.Events;

public record AccountCreate
{
    public required Account Account { get; set; }

    public override string ToString()
    {
        return $"Create new account, owner = {Account.Owner}, balance = {Account.Balance}";
    }
}

public record AccountUpdate
{
    public required Account Account { get; set; }

    public override string ToString()
    {
        return $"Account update, new address = {Account.Address}";
    }
}

public record DepositEvent
{
    public required decimal Amount { get; set; }

    public override string ToString()
    {
        return $"Deposit {Amount} to the account";
    }
}

public record WithdrawEvent
{
    public required decimal Amount { get; set; }

    public override string ToString()
    {
        return $"Withdraw {Amount} from account";
    }
}

public record PaymentEvent
{
    public required string ServiceName { get; set; }
    public required decimal Amount { get; set; }

    public override string ToString()
    {
        return $"Paid bill {ServiceName}: {Amount}";
    }
}
