using BankAccountEventSource.Controller;
using BankAccountEventSource.Events;
using BankAccountEventSource.Mapper;
using BankAccountEventSource.Models;
using BankAccountEventSource.Persistence;
using Marten;
using MediatR;

namespace BankAccountEventSource.Cqrs.Command;

public record CreatePayServiceCommand(string AccountId, string ServiceName, decimal Amount) : IRequest;

public class CreatePayServiceCommandHandler : IRequestHandler<CreatePayServiceCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IDocumentStore _store;

    public CreatePayServiceCommandHandler(AppDbContext dbContext, IDocumentStore store)
    {
        _dbContext = dbContext;
        _store = store;
    }

    public async Task Handle(CreatePayServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Accounts.FindAsync(request.AccountId, cancellationToken);

        if (entity == null)
        {
            throw new ArgumentException($"Account with Id {request.AccountId} Not Found");
        }

        var paymentEvent = new PaymentEvent
        {
            ServiceName = request.ServiceName,
            Amount = request.Amount
        };

        await using var session = _store.LightweightSession();
        session.Events.Append(entity.StreamKey, paymentEvent);
        await session.SaveChangesAsync(cancellationToken);

        entity.Balance -= request.Amount;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
