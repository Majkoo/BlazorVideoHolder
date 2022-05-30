using VideoHolder.Data.Entities;
using VideoHolder.Data.Entities.Abstract;
using VideoHolder.View.Interfaces;
using VideoHolder.View.Services;
using VideoHolder.View.Subject;

namespace VideoHolder.View.Shared.Admin.EditItems;

public partial class EditHomeItem : ISubscriber, IDisposable
{
    public EditHomeItem()
    {
        DeletedItemSubject.Subscribe(this);
    }

    public async void Update(Item context)
    {
        _items.Remove(context as HomeItem);
        await _grid.Reload();
    }
    public void Dispose()
    {
        DeletedItemSubject.Unsubscribe(this);
    }


    private async void OpenDeleteDialog(HomeItem data)
    {
        await DialogService.OpenAsync<DeleteItemDialog>(
            "Usuń zasób",
            new Dictionary<string, object>()
            {
                {
                    "Item", data
                }
            }
        );
    }

    private void OpenEditDialog(HomeItem data)
    {
        NotificationService.Notify(DictionaryService.NotificationMessages["NotImplemented"]);
    }

}