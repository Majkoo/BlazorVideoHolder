using Microsoft.EntityFrameworkCore;
using VideoHolder.Data.Entities;
using VideoHolder.Data.Repos.Interfaces;

namespace VideoHolder.Data.Repos;


public class ItemRepo : IItemRepo
{
    private readonly HolderDbContext _ctx;

    public ItemRepo(
        HolderDbContext ctx)
    {
        _ctx = ctx;
    }

    #region GetItemsMethods

    public async Task<IEnumerable<HomeItem>> GetHomeItems()
    {
        return await _ctx.HomeItems
            .OrderByDescending(i => i.PostDateTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<AdvItem>> GetAdvItems()
    {
        return await _ctx.AdvItems
            .OrderByDescending(i => i.PostDateTime)
            .ToListAsync();
    }

    #endregion
    
    #region SaveItemsMethods

    public async Task<HomeItem> InsertHomeItem(HomeItem item)
    {
        await _ctx.HomeItems.AddAsync(item);
        return item;
    }

    public async Task<AdvItem> InsertAdvItem(AdvItem item)
    {
        await _ctx.AdvItems.AddAsync(item);
        return item;
    }

    public async Task SaveChanges()
    {
        await _ctx.SaveChangesAsync();
    }

    #endregion

    #region DeleteItemsMethods

    public async Task<bool> DeleteHomeItem(HomeItem item)
    {
        _ctx.HomeItems.Remove(item);
        return await _ctx.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAdvItem(AdvItem item)
    {
        _ctx.AdvItems.Remove(item);
        return await _ctx.SaveChangesAsync() > 0;
    }

    #endregion
}