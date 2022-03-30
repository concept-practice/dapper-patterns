namespace DapperPatterns.Domain
{
    public abstract class GuidSqlBuilder<TEntity> : SqlBuilder<TEntity, Guid>
        where TEntity : IEntity<Guid>
    {
        protected GuidSqlBuilder(string tableName)
            : base(tableName)
        {
        }

        protected GuidSqlBuilder()
        {
            
        }
    }
}
