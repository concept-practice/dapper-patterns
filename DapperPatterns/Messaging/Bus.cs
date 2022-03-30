namespace DapperPatterns.Messaging
{
    public class Bus : IBus
    {
        public Task Publish(INotification notification)
        {
            Console.WriteLine($"Got notification: {notification.GetType()}");

            return Task.CompletedTask;
        }
    }
}
