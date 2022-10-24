using Microsoft.EntityFrameworkCore;
using VideoHolder.Data.Entities;
using VideoHolder.Data.Repos.Interfaces;

namespace VideoHolder.Data.Repos;

public class AccountRepo: IAccountRepo
{
    private readonly HolderDbContext _ctx;

    public AccountRepo(
        HolderDbContext ctx
        )
    {
        _ctx = ctx;
    }

    public async Task<Account> GetAccountByLoginAsync(string login)
    {
        return await _ctx.Accounts.SingleOrDefaultAsync(a => a.Login == login);
    }

    public async Task<Account> Create(Account account)
    {
        _ctx.Accounts.Add(account);
        await _ctx.SaveChangesAsync();
        return account;
    }

    public async Task<bool> IsAnyAccountPresent()
    {
        return await _ctx.Accounts.AnyAsync();
    }

    public bool Delete(Account account)
    {
        _ctx.Accounts.Remove(account);
        _ctx.SaveChanges();
        return true;
    }

}