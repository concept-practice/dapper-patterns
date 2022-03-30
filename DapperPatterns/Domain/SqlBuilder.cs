namespace DapperPatterns.Domain
{
    public abstract class SqlBuilder<T, TId>
        where T : IEntity<TId>
    {
        private readonly string _tableName;
        private string _query;

        protected SqlBuilder(string tableName)
        {
            _tableName = tableName;
        }

        protected SqlBuilder()
        {
            _tableName = $"dbo.{typeof(T).Name}";
        }

        public string Query
        {
            get
            {
                var toReturn = _query;
                _query = string.Empty;
                return toReturn;
            }
        }

        protected abstract Func<T, IEnumerable<string>> EntityProperties { get; }

        protected virtual string DefaultInclude { get; }

        public SqlBuilder<T, TId> SelectAll()
        {
            _query = $"SELECT * FROM {_tableName} ";

            if (DefaultInclude != string.Empty)
            {
                _query += DefaultInclude;
            }

            return this;
        }

        public SqlBuilder<T, TId> GetById(TId id)
        {
            SelectAll();

            _query += $" WHERE {_tableName}.Id = \'{id}\' ";

            return this;
        }

        public SqlBuilder<T, TId> Insert(T entity)
        {
            _query += $" INSERT into {_tableName} VALUES ({EntityProperties.Invoke(entity).Aggregate(string.Empty, (final, next) => final + $"'{next}', ").Trim().TrimEnd(',')})";

            return this;
        }

        public SqlBuilder<T, TId> InsertMultiple(IEnumerable<T> entities)
        {
            _query += $" INSERT into {_tableName} VALUES" +
                      $" {entities.Aggregate(string.Empty, (final, next) => final + $"({InsertStatement(next)}), ")}";
            _query = _query.Trim().TrimEnd(',');

            return this;
        }

        private string InsertStatement(T entity)
        {
            return EntityProperties.Invoke(entity).Aggregate(string.Empty, (final, next) => final + $"'{next}', ").Trim().TrimEnd(',');
        }

        public SqlBuilder<T, TId> Delete(T entity)
        {
            _query += $" DELETE FROM {_tableName} WHERE Id = '{entity.Id}' ";

            return this;
        }

        public SqlBuilder<T, TId> InnerJoin(string table, string foreignKey)
        {
            _query += $" INNER JOIN {table} on {foreignKey} = {table}.Id";

            return this;
        }

        public SqlBuilder<T, TId> And(string sql)
        {
            _query += $" AND {sql}";

            return this;
        }
    }
}
