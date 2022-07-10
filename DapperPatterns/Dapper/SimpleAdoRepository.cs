using System.Data.SqlClient;
using DapperPatterns.Common;
using DapperPatterns.Domain;

namespace DapperPatterns.Dapper
{
    public abstract class SimpleAdoRepository<TEntity> :
        IGetAll<TEntity>,
        IAddEntity<TEntity>,
        IGetById<TEntity>,
        IDeleteEntity<TEntity>,
        IAddEntities<TEntity>
        where TEntity : IEntity<Guid>
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlBuilder<TEntity, Guid> _sqlBuilder;
        private readonly Func<SqlDataReader, TEntity> _conversionFunc;

        protected SimpleAdoRepository(SqlConnection sqlConnection, SqlBuilder<TEntity, Guid> sqlBuilder, Func<SqlDataReader, TEntity> conversionFunc)
        {
            _sqlConnection = sqlConnection;
            _sqlBuilder = sqlBuilder;
            _conversionFunc = conversionFunc;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await ExecuteQueryAsync(_sqlBuilder.SelectAll().Query);
        }

        public virtual async Task<int> AddEntity(TEntity entity)
        {
            return await ExecuteNonQueryAsync(_sqlBuilder.Insert(entity).Query);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var result = await ExecuteQueryAsync(_sqlBuilder.GetById(id).Query);

            return result.First();
        }

        public async Task<int> DeleteEntity(TEntity entity)
        {
            return await ExecuteNonQueryAsync(_sqlBuilder.Delete(entity).Query);
        }

        public async Task<int> AddEntities(IEnumerable<TEntity> entities)
        {
            return await ExecuteNonQueryAsync(_sqlBuilder.InsertMultiple(entities).Query);
        }

        protected async Task<int> ExecuteNonQueryAsync(string command, CancellationToken cancellationToken = default)
        {
            await _sqlConnection.OpenAsync(cancellationToken);

            var sqlCommand = new SqlCommand(command, _sqlConnection);

            var result = await sqlCommand.ExecuteNonQueryAsync(cancellationToken);

            await _sqlConnection.CloseAsync();

            return result;
        }

        protected async Task<List<TEntity>> ExecuteQueryAsync(string command, Func<SqlDataReader, TEntity>? overloadDefaultConversion = default, CancellationToken cancellationToken = default)
        {
            var sqlQuery = new SqlCommand(command, _sqlConnection);

            await _sqlConnection.OpenAsync(cancellationToken);

            var sqlDataReader = await sqlQuery.ExecuteReaderAsync(cancellationToken);

            var result = new List<TEntity>();

            var conversionFunc = overloadDefaultConversion ?? _conversionFunc;

            while (await sqlDataReader.ReadAsync(cancellationToken))
            { 
                result.Add(conversionFunc.Invoke(sqlDataReader));
            }

            await _sqlConnection.CloseAsync();

            return result;
        }
    }
}
