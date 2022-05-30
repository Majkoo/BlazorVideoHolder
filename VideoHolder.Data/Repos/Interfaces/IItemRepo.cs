using VideoHolder.Data.Entities;

namespace VideoHolder.Data.Repos.Interfaces;

public interface IItemRepo
{
    Task<IEnumerable<HomeItem>> GetHomeItems();
    Task<IEnumerable<AdvItem>> GetAdvItems();
    Task<HomeItem> InsertHomeItem(HomeItem item);
    Task<AdvItem> InsertAdvItem(AdvItem item);
    Task<AdvItem> UpdateAdvItem(AdvItem item);
    Task<HomeItem> UpdateHomeItem(HomeItem item);
    Task SaveChanges();
    Task<bool> DeleteHomeItem(HomeItem item);
    Task<bool> DeleteAdvItem(AdvItem item);
}