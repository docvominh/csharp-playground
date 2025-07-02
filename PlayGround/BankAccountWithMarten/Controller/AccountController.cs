using BankAccountWithMarten.Cqrs.Command;
using BankAccountWithMarten.Cqrs.Query;
using BankAccountWithMarten.EventSource;
using BankAccountWithMarten.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountWithMarten.Controller;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ISender _sender;

    public AccountController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{accountId}")]
    public async Task<AccountAggregate> Get(string accountId)
    {
        return await _sender.Send(new GetAccountQuery(new Guid(accountId)));
    }

    [HttpGet("{accountId}/history")]
    public async Task<AccountHistory> GetHistory(string accountId)
    {
        return await _sender.Send(new GetAccountHistoryQuery(new Guid(accountId)));
    }

    [HttpPost]
    public async Task<AccountAggregate> Create([FromBody] AccountModel account)
    {
        return await _sender.Send(new CreateAccountCommand(account));
    }

    [HttpPut("{accountId}")]
    public async Task<AccountAggregate> Update(Guid accountId, string address)
    {
        return await _sender.Send(new UpdateAccountCommand(accountId, address));
    }

    [HttpPost("{accountId}/deposit")]
    public async Task DepositService(Guid accountId, decimal amount)
    {
        await _sender.Send(new WithdrawCommand(accountId, amount));
    }

    [HttpPost("{accountId}/withdraw")]
    public async Task WithdrawService(Guid accountId, decimal amount)
    {
        await _sender.Send(new WithdrawCommand(accountId, amount));
    }

    [HttpPost("/rebuild")]
    public async Task RebuildProjection(string projectionType)
    {
        await _sender.Send(new RebuildProjectionCommand(projectionType));
    }
}
