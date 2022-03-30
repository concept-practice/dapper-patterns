namespace DapperPatterns.Messaging
{
    public interface IBus
    {
        Task Publish(INotification notification);
    }
}
