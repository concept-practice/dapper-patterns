using DapperPatterns.Domain;

namespace DapperPatterns.Common
{
    public interface ICustomQuery<TEntity>
    where TEntity : IEntity<Guid>
    {
        Task<List<TEntity>> CustomQuery<TCustomerRecord>(string sql, Func<TCustomerRecord, TEntity> mappingFunc);
    }
}
