using VideoHolder.Data.Entities;

namespace VideoHolder.Data.Repos.Interfaces;

public interface IAccountRepo
{
    Task<Account> GetAccountByLoginAsync(string login);
    Task<Account> Create(Account account);
    Task<bool> IsAnyAccountPresent();
    bool Delete(Account account);
}