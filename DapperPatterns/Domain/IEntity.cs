namespace DapperPatterns.Domain
{
    public interface IEntity<out TId> where TId : struct
    {
        TId Id { get; }
    }
}
