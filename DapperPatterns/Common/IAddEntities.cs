using DapperPatterns.Domain;

namespace DapperPatterns.Common
{
    public interface IAddEntities<in T>
        where T : IEntity<Guid>
    {
        Task<int> AddEntities(IEnumerable<T> entities);
    }
}
