using BankAccountEventSource.Controller;
using BankAccountEventSource.Events;
using BankAccountEventSource.Mapper;
using BankAccountEventSource.Models;
using BankAccountEventSource.Persistence;
using Marten;
using MediatR;

namespace BankAccountEventSource.Cqrs.Command;

public record CreateAccountCommand(Account Account) : IRequest<Account>;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
{
    private readonly AppDbContext _dbContext;
    private readonly IDocumentStore _store;
    private readonly IAccountMapper _mapper;

    public CreateAccountCommandHandler(AppDbContext dbContext, IDocumentStore store, IAccountMapper mapper)
    {
        _dbContext = dbContext;
        _store = store;
        _mapper = mapper;
    }

    public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        // Open a session for querying, loading, and updating documents
        await using var session = _store.LightweightSession();

        var streamKey = Guid.NewGuid();

        request.Account.Id = $"account/{Guid.NewGuid()}";

        var createAccountEvent = new AccountCreate
        {
            Account = request.Account,
        };
        createAccountEvent.Account.Balance = 0;

        var depositEvent = new DepositEvent
        {
            Amount = request.Account.Balance
        };

        session.Events.StartStream<AccountActivity>(streamKey, createAccountEvent, depositEvent);

        await session.SaveChangesAsync(cancellationToken);

        var entity = _mapper.ToEntity(request.Account);
        entity.StreamKey = streamKey;

        _dbContext.Accounts.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.ToModel(entity);
    }
}
