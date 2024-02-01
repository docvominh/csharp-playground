using BankAccountEventSource.Controller;
using BankAccountEventSource.Mapper;
using BankAccountEventSource.Models;
using BankAccountEventSource.Persistence;
using Marten;
using MediatR;

namespace BankAccountEventSource.Cqrs.Query;

public record GetAccountQuery(string AccountId) : IRequest<AccountView>;

public class GetAccountQueryHandle : IRequestHandler<GetAccountQuery, AccountView>
{
    private readonly AppDbContext _dbContext;
    private readonly IDocumentStore _store;

    public GetAccountQueryHandle(AppDbContext dbContext, IDocumentStore store)
    {
        _dbContext = dbContext;
        _store = store;
    }

    public async Task<AccountView> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Accounts.FindAsync(request.AccountId, cancellationToken);

        if (entity == null)
        {
            throw new ArgumentException($"Account with Id {request.AccountId} Not Found");
        }

        await using var session = _store.LightweightSession();

        var events = await session.Events.FetchStreamAsync(entity.StreamKey, token: cancellationToken);

        AccountView account = new AccountView();

        foreach (var e in events)
        {
            account.AddEvent(e);
        }

        // var account =
        //     await session.Events.AggregateStreamAsync<AccountView>(entity.StreamKey, token: cancellationToken) ??
        //     throw new InvalidOperationException();

        return account;
    }
}
