using System.Text.Json.Serialization;
using BankAccountEventSource.Events;
using Marten.Events;

namespace BankAccountEventSource.Models;

public class Account
{
    public string? Id { get; set; }
    public required string Owner { get; set; }
    public string? Address { get; set; }
    public required decimal Balance { get; set; }
}

public class AccountView
{
    public string? Id { get; set; }
    public string? Owner { get; set; }
    public string? Address { get; set; }
    public decimal Balance { get; set; }

    public List<string> Events { get; set; } = new();

    public void Apply(AccountCreate @event)
    {
        Id = @event.Account.Id;
        Owner = @event.Account.Owner;
        Address = @event.Account.Address;
    }

    public void Apply(AccountUpdate @event) => Address = @event.Account.Address;

    public void Apply(DepositEvent @event) => Balance += @event.Amount;

    public void Apply(WithdrawEvent @event) => Balance -= @event.Amount;
    public void Apply(PaymentEvent @event) => Balance -= @event.Amount;

    public void AddEvent(IEvent @event)
    {

        var eventType = @event.EventType;

        if (eventType == typeof(AccountCreate))
        {
            AccountCreate e = (AccountCreate)@event.Data ?? throw new InvalidOperationException();
            Apply(e);
            Events.Add(e.ToString());
            return;
        }

        if (eventType == typeof(AccountUpdate))
        {
            AccountUpdate e = (AccountUpdate)@event.Data ?? throw new InvalidOperationException();
            Apply(e);
            Events.Add(e.ToString());
            return;
        }

        if (eventType == typeof(DepositEvent))
        {
            DepositEvent deposit = (DepositEvent)@event.Data ?? throw new InvalidOperationException();
            Apply(deposit);
            Events.Add(deposit.ToString());
            return;
        }

        if (eventType == typeof(PaymentEvent))
        {
            PaymentEvent payment = (PaymentEvent)@event.Data ?? throw new InvalidOperationException();
            Apply(payment);
            Events.Add(payment.ToString());
            return;
        }

        if (eventType == typeof(WithdrawEvent))
        {
            WithdrawEvent withdraw = (WithdrawEvent)@event.Data ?? throw new InvalidOperationException();
            Apply(withdraw);
            Events.Add(withdraw.ToString());
        }
    }
}
