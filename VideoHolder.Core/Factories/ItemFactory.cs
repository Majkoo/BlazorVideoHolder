using Microsoft.AspNetCore.Components.Forms;
using VideoHolder.Core.Factories.Interfaces;
using VideoHolder.Data.Entities;
using VideoHolder.Data.Entities.Abstract;
using VideoHolder.Data.Repos.Interfaces;
using VideoHolder.Shared.Dtos;
using VideoHolder.Shared.Enums;

namespace VideoHolder.Core.Factories;

public class ItemFactory: IItemFactory
{
    private readonly IItemRepo _itemRepo;

    public ItemFactory(
        IItemRepo itemRepo
        )
    {
        _itemRepo = itemRepo;
    }

    public async Task<IEnumerable<HomeItem>> GetHomeItems()
    {
        return await _itemRepo.GetHomeItems();
    }

    public async Task<IEnumerable<AdvItem>> GetAdvItems()
    {
        return await _itemRepo.GetAdvItems();
    }

    public async Task<Item> CreateItem(CreateItemDto createItemDto, IBrowserFile browserFile, ItemOption itemOption)
    {
        CheckPaths();
        return itemOption switch
        {
            ItemOption.Home => await PostHomeItem(createItemDto, browserFile),
            ItemOption.Advs => await PostAdvItem(createItemDto, browserFile),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<Item> UpdateItem(Item item)
    {
        CheckPaths();
        if (typeof(HomeItem) == item.GetType())
        {
            return await _itemRepo.UpdateHomeItem(item as HomeItem);
        }
        if (typeof(AdvItem) == item.GetType())
        {
            return await _itemRepo.UpdateAdvItem(item as AdvItem);
        }
        throw new ArgumentOutOfRangeException();
    }

    public async Task<bool> DeleteItem(Item item)
    {
        CheckPaths();
        try
        {
            if (item.GetType() == typeof(HomeItem))
            {
                await _itemRepo.DeleteHomeItem(item as HomeItem);
            }
            else if (item.GetType() == typeof(AdvItem))
            {
                await _itemRepo.DeleteAdvItem(item as AdvItem);
            }
            var delPath = Path.Combine("wwwroot", item.Url);
            File.Delete(delPath);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #region private methods

    private async Task<HomeItem> PostHomeItem(CreateItemDto createItemDto, IBrowserFile browserFile)
    {
        var itemGuid = Guid.NewGuid();
        var fileName = $"{itemGuid}.{browserFile.Name.Split('.').Last()}";

        var homeItem = new HomeItem()
        {
            Id = itemGuid,
            Url = Path.Combine("items", "home", fileName),
            ItemType = browserFile.ContentType.StartsWith("image")
                ? ItemType.Picture
                : ItemType.Video,
            Name = createItemDto.Name,
            Description = createItemDto.Description
        };
        await _itemRepo.InsertHomeItem(homeItem);

        var path = Path.Combine("wwwroot","items", "home", fileName);
        await using FileStream fs = new(path, FileMode.Create);
        await browserFile.OpenReadStream(104857600).CopyToAsync(fs);

        await _itemRepo.SaveChanges();
        return homeItem;
    }

    private async Task<AdvItem> PostAdvItem(CreateItemDto createItemDto, IBrowserFile browserFile)
    {
        var itemGuid = Guid.NewGuid();
        var fileName = $"{itemGuid}.{browserFile.Name.Split('.').Last()}";

        var advItem = new AdvItem()
        {
            Id = itemGuid,
            Url = Path.Combine("items", "adv", fileName),
            Name = createItemDto.Name,
            Description = createItemDto.Description
        };
        await _itemRepo.InsertAdvItem(advItem);

        var path = Path.Combine("wwwroot","items", "adv", fileName);
        await using FileStream fs = new(path, FileMode.Create);
        await browserFile.OpenReadStream(104857600).CopyToAsync(fs);

        await _itemRepo.SaveChanges();
        return advItem;
    }

    private static void CheckPaths()
    {
        var itemsPath = Path.Combine("wwwroot", "items");
        var advPath = Path.Combine(itemsPath, "adv");
        var homePath = Path.Combine(itemsPath, "home");

        if (!Directory.Exists(itemsPath)) Directory.CreateDirectory(itemsPath);
        if (!Directory.Exists(advPath)) Directory.CreateDirectory(advPath);
        if (!Directory.Exists(homePath)) Directory.CreateDirectory(homePath);
    }

    #endregion
}