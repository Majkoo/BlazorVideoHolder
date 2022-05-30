using VideoHolder.Data.Entities.Abstract;
using VideoHolder.View.Interfaces;

namespace VideoHolder.View.Subject;

public static class DeletedItemSubject
{
    private static readonly List<ISubscriber> Subscribers = new();

    public static void Subscribe(ISubscriber subscriber)
    {
        Subscribers.Add(subscriber);
    }

    public static void Unsubscribe(ISubscriber subscriber)
    {
        Subscribers.Remove(subscriber);
    }

    public static void Next(Item context)
    {
        foreach (var subscriber in Subscribers)
        {
            subscriber.Update(context);
        }
    }

}