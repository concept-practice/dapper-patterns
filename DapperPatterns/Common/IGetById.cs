using DapperPatterns.Domain;

namespace DapperPatterns.Common
{
    public interface IGetById<TEntity>
        where TEntity : IEntity<Guid>
    {
        Task<TEntity> GetById(Guid id);
    }
}
