using BankAccountWithMarten.EventSource;
using BankAccountWithMarten.EventSource.Events;
using BankAccountWithMarten.Models;
using Marten;
using MediatR;

namespace BankAccountWithMarten.Cqrs.Command;

public record CreateAccountCommand(AccountModel Account) : IRequest<AccountAggregate>;

internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountAggregate>
{
    private readonly IDocumentStore _store;

    public CreateAccountCommandHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<AccountAggregate> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        await using IDocumentSession session = _store.LightweightSession();

        Guid streamKey = Guid.NewGuid();

        Account account = new ()
        {
            Id = streamKey,
            Owner = request.Account.Owner,
            Balance = request.Account.Balance,
            Address = request.Account.Address,
        };

        AccountCreate createAccountEvent = new ()
        {
            Account = account
        };

        createAccountEvent.Account.Balance = 0;

        DepositEvent depositEvent = new ()
        {
            Amount = request.Account.Balance
        };

        session.Events.Append(streamKey, createAccountEvent, depositEvent);

        await session.SaveChangesAsync(cancellationToken);

        return await session.LoadAsync<AccountAggregate>(streamKey, cancellationToken)
            ?? throw new InvalidOperationException();
    }
}
