using BankAccountWithMarten.EventSource;
using Marten;
using MediatR;

namespace BankAccountWithMarten.Cqrs.Query;

public record GetAccountQuery(Guid AccountId) : IRequest<AccountAggregate>;

public class GetAccountQueryHandle : IRequestHandler<GetAccountQuery, AccountAggregate>
{
    private readonly IDocumentStore _store;

    public GetAccountQueryHandle(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<AccountAggregate> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        await using IDocumentSession session = _store.LightweightSession();

        return await session.LoadAsync<AccountAggregate>(request.AccountId, cancellationToken)
            ?? throw new InvalidOperationException();
    }
}
