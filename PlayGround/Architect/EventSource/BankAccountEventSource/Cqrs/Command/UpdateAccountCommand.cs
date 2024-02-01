using BankAccountEventSource.Controller;
using BankAccountEventSource.Events;
using BankAccountEventSource.Mapper;
using BankAccountEventSource.Models;
using BankAccountEventSource.Persistence;
using Marten;
using MediatR;

namespace BankAccountEventSource.Cqrs.Command;

public record UpdateAccountCommand(string AccountId, Account Account) : IRequest<Account>;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Account>
{
    private readonly AppDbContext _dbContext;
    private readonly IDocumentStore _store;
    private readonly IAccountMapper _mapper;

    public UpdateAccountCommandHandler(AppDbContext dbContext, IDocumentStore store, IAccountMapper mapper)
    {
        _dbContext = dbContext;
        _store = store;
        _mapper = mapper;
    }

    public async Task<Account> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Accounts.FindAsync(request.AccountId);

        if (entity == null)
        {
            throw new ArgumentException($"Account with Id {request.AccountId} Not Found");
        }


        var updateAccountEvent = new AccountUpdate
        {
            Account = request.Account,
        };

        await using var session = _store.LightweightSession();

        session.Events.Append(entity.StreamKey, updateAccountEvent);

        await session.SaveChangesAsync(cancellationToken);


        entity.Address = request.Account.Address;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.ToModel(entity);
    }
}
