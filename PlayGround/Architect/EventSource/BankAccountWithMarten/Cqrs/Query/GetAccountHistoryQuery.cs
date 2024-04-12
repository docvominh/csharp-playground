using BankAccountWithMarten.EventSource;
using Marten;
using MediatR;

namespace BankAccountWithMarten.Cqrs.Query;

public record GetAccountHistoryQuery(Guid AccountId) : IRequest<AccountHistory>;

public class GetAccountHistoryQueryHandle : IRequestHandler<GetAccountHistoryQuery, AccountHistory>
{
    private readonly IDocumentStore _store;

    public GetAccountHistoryQueryHandle(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<AccountHistory> Handle(GetAccountHistoryQuery request, CancellationToken cancellationToken)
    {
        await using IDocumentSession session = _store.LightweightSession();

        return await session.LoadAsync<AccountHistory>(request.AccountId, cancellationToken)
            ?? throw new InvalidOperationException();
    }
}
