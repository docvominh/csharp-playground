using System.Web;
using BankAccountEventSource.Cqrs.Command;
using BankAccountEventSource.Cqrs.Query;
using BankAccountEventSource.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountEventSource.Controller;

[Controller]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ISender _sender;

    public AccountController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{accountId}")]
    public async Task<AccountView> Get(string accountId)
    {
        return await _sender.Send(new GetAccountQuery(HttpUtility.UrlDecode(accountId)));
    }

    [HttpPost]
    public async Task<Account> Create([FromBody] Account account)
    {
        return await _sender.Send(new CreateAccountCommand(account));
    }

    [HttpPut("{accountId}")]
    public async Task<Account> Update(string accountId, [FromBody] Account account)
    {
        return await _sender.Send(new UpdateAccountCommand(HttpUtility.UrlDecode(accountId), account));
    }

    [HttpPost("{accountId}/pay")]
    public async Task PayService(string accountId, string serviceName, decimal amount)
    {
        await _sender.Send(new CreatePayServiceCommand(HttpUtility.UrlDecode(accountId), serviceName, amount));
    }

    [HttpPost("{accountId}/withdraw")]
    public async Task WithdrawService(string accountId, decimal amount)
    {
        await _sender.Send(new CreateWithdrawCommand(HttpUtility.UrlDecode(accountId), amount));
    }
}
