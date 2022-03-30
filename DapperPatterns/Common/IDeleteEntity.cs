using DapperPatterns.Domain;

namespace DapperPatterns.Common
{
    public interface IDeleteEntity<in T>
        where T : IEntity<Guid>
    {
        Task<int> DeleteEntity(T entity);
    }
}
