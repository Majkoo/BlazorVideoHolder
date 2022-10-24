using VideoHolder.Data.Entities.Abstract;
using VideoHolder.Shared.Enums;

namespace VideoHolder.Data.Entities;

public class HomeItem : Item
{
    public ItemType ItemType { get; set; }
}