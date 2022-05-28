using Microsoft.AspNetCore.Components.Forms;
using VideoHolder.Core.Dtos;
using VideoHolder.Core.Enums;
using VideoHolder.Core.Factories.Interfaces;
using VideoHolder.Data.Entities;
using VideoHolder.Data.Entities.Abstract;
using VideoHolder.Data.Enums;
using VideoHolder.Data.Repos.Interfaces;

namespace VideoHolder.Core.Factories;

public class ItemFactory: IItemFactory
{
    private readonly IItemRepo _itemRepo;

    public ItemFactory(
        IItemRepo itemRepo)
    {
        _itemRepo = itemRepo;
    }

    public async Task<IEnumerable<Item>> GetItems(ItemOption itemOption)
    {
        switch (itemOption)
        {
            case ItemOption.Home:
                return await _itemRepo.GetHomeItems();

            case ItemOption.Advs:
                return await _itemRepo.GetAdvItems();

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task<Item> CreateItem(CreateItemDto createItemDto, IBrowserFile browserFile, ItemOption itemOption)
    {
        try
        {
            return itemOption switch
            {
                ItemOption.Home => await PostHomeItem(createItemDto, browserFile),
                ItemOption.Advs => await PostAdvItem(createItemDto, browserFile),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteItem()
    {
        throw new NotImplementedException();
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

    #endregion
}