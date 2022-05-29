using Microsoft.AspNetCore.Components.Forms;
using VideoHolder.Core.Dtos;
using VideoHolder.Core.Enums;
using VideoHolder.Data.Entities;
using VideoHolder.Data.Entities.Abstract;

namespace VideoHolder.Core.Factories.Interfaces;

public interface IItemFactory
{
    Task<IEnumerable<HomeItem>> GetHomeItems();
    Task<IEnumerable<AdvItem>> GetAdvItems();
    Task<Item> CreateItem(CreateItemDto createItemDto, IBrowserFile browserFile, ItemOption itemOption);
    Task<bool> DeleteItem();
}