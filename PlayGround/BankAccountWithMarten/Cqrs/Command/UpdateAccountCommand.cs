using BankAccountWithMarten.EventSource;
using BankAccountWithMarten.EventSource.Events;
using Marten;
using Marten.Events;
using MediatR;

namespace BankAccountWithMarten.Cqrs.Command;

public record UpdateAccountCommand(Guid AccountId, string Address) : IRequest<AccountAggregate>;

internal class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountAggregate>
{
    private readonly IDocumentStore _store;

    public UpdateAccountCommandHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<AccountAggregate> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        await using IDocumentSession session = _store.LightweightSession();

        StreamState? state = await session.Events.FetchStreamStateAsync(request.AccountId, cancellationToken);

        if (state == null)
        {
            throw new ArgumentException($"Account with Id {request.AccountId} Not Found");
        }

        AccountUpdate updateAccountEvent = new ()
        {
            Address = request.Address
        };

        session.Events.Append(request.AccountId, updateAccountEvent);

        await session.SaveChangesAsync(cancellationToken);

        return await session.LoadAsync<AccountAggregate>(request.AccountId, cancellationToken)
            ?? throw new InvalidOperationException();
    }
}
