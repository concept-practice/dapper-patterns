using DapperPatterns.Domain;

namespace DapperPatterns.Common
{
    public interface IGetAll<TEntity> 
        where TEntity: IEntity<Guid>
    {
        Task<List<TEntity>> GetAll();
    }
}
