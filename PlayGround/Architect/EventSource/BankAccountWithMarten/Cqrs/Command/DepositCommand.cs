using BankAccountWithMarten.EventSource;
using BankAccountWithMarten.EventSource.Events;
using Marten;
using Marten.Events;
using MediatR;

namespace BankAccountWithMarten.Cqrs.Command;

public record DepositCommand(string AccountId, decimal Amount) : IRequest<AccountAggregate>;

internal class DepositCommandHandler : IRequestHandler<DepositCommand, AccountAggregate>
{
    private readonly IDocumentStore _store;

    public DepositCommandHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<AccountAggregate> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        await using IDocumentSession session = _store.LightweightSession();

        StreamState? state = await session.Events.FetchStreamStateAsync(request.AccountId, cancellationToken);

        if (state == null)
        {
            throw new ArgumentException($"Account with Id {request.AccountId} Not Found");
        }

        var depositEvent = new DepositEvent
        {
            Amount = request.Amount
        };

        session.Events.Append(request.AccountId, depositEvent);

        await session.SaveChangesAsync(cancellationToken);

        return await session.LoadAsync<AccountAggregate>(request.AccountId, cancellationToken)
            ?? throw new InvalidOperationException();
    }
}
