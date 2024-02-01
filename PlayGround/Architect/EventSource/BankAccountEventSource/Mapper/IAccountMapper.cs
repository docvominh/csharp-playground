using BankAccountEventSource.Controller;
using BankAccountEventSource.Models;
using BankAccountEventSource.Persistence.Entity;
using Riok.Mapperly.Abstractions;

namespace BankAccountEventSource.Mapper;

public interface IAccountMapper
{
    public Account ToModel(AccountEntity entity);

    // [MapperIgnoreTarget(nameof(AccountEntity.Id))]
    public AccountEntity ToEntity(Account entity);

    public List<Account> ToModels(List<AccountEntity> entities);
}

[Mapper]
public partial class AccountMapper : IAccountMapper
{
    public partial Account ToModel(AccountEntity entity);

    public partial AccountEntity ToEntity(Account entity);

    public partial List<Account> ToModels(List<AccountEntity> entities);
}
