using DapperPatterns.Messaging;

namespace DapperPatterns.Domain
{
    public abstract class Entity : IEntity<Guid>
    {
        private readonly List<INotification> _notifications;

        public IEnumerable<INotification> Notifications => _notifications;

        protected Entity(Guid id)
        {
            Id = id;
            _notifications = new List<INotification>();
        }

        public Guid Id { get; }

        protected void AddNotification(INotification notification)
        {
            _notifications.Add(notification);
        }
    }
}
