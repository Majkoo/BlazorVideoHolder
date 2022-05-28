using Microsoft.EntityFrameworkCore;
using VideoHolder.Data.Entities;

namespace VideoHolder.Data;

public class HolderDbContext: DbContext
{
    public HolderDbContext(DbContextOptions<HolderDbContext> options) : base(options) { }

    public DbSet<HomeItem> HomeItems { get; set; }
    public DbSet<AdvItem> AdvItems { get; set; }
    public DbSet<Account> Accounts { get; set; }

}