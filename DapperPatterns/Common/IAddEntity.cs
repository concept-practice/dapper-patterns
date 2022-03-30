using DapperPatterns.Domain;

namespace DapperPatterns.Common
{
    public interface IAddEntity<in TEntity>
    where TEntity : IEntity<Guid>
    {
        Task<int> AddEntity(TEntity entity);
    }
}
