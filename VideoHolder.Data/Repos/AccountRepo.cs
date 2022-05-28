using VideoHolder.Data.Entities;

namespace VideoHolder.Data.Repos;

public class AccountRepo
{
    private readonly HolderDbContext _ctx;

    public AccountRepo(
        HolderDbContext ctx)
    {
        _ctx = ctx;
    }

    public Account Create(Account account)
    {
        _ctx.Accounts.Add(account);
        _ctx.SaveChanges();
        return account;
    }

    // public Account Login(int id, string login, string passHash)
    // {
    //
    // }

    public bool Delete(Account account)
    {
        _ctx.Accounts.Remove(account);
        _ctx.SaveChanges();
        return true;
    }

}