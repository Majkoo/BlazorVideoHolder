using VideoHolder.Data.Entities.Abstract;

namespace VideoHolder.View.Interfaces;

public interface ISubscriber
{
    void Update(Item context);
}