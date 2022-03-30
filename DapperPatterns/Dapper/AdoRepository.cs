using System.Data.SqlClient;
using DapperPatterns.Common;
using DapperPatterns.Domain;

namespace DapperPatterns.Dapper
{
    public abstract class AdoRepository<TEntity> :
        IGetAll<TEntity>,
        IAddEntity<TEntity>,
        IGetById<TEntity>,
        IDeleteEntity<TEntity>,
        IAddEntities<TEntity>
        where TEntity : IEntity<Guid>
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlBuilder<TEntity, Guid> _sqlBuilder;
        private readonly Func<IList<object>, TEntity> _conversionFunc;

        protected AdoRepository(SqlConnection sqlConnection, SqlBuilder<TEntity, Guid> sqlBuilder, Func<IList<object>, TEntity> conversionFunc)
        {
            _sqlConnection = sqlConnection;
            _sqlBuilder = sqlBuilder;
            _conversionFunc = conversionFunc;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await ExecuteQueryAsync(_sqlBuilder.SelectAll().Query);
        }

        public async Task<int> AddEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddEntities(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        protected async Task<int> ExecuteNonQueryAsync(string command, CancellationToken cancellationToken = default)
        {
            await _sqlConnection.OpenAsync(cancellationToken);

            var sqlCommand = new SqlCommand(command, _sqlConnection);

            var result = await sqlCommand.ExecuteNonQueryAsync(cancellationToken);

            await _sqlConnection.CloseAsync();

            return result;
        }

        protected async Task<List<TEntity>> ExecuteQueryAsync(string command, Func<IEnumerable<object>, TEntity> overloadDefaultConversion = default, CancellationToken cancellationToken = default)
        {
            var sqlQuery = new SqlCommand(command, _sqlConnection);

            await _sqlConnection.OpenAsync(cancellationToken);

            var sqlDataReader = await sqlQuery.ExecuteReaderAsync(cancellationToken);

            var result = new List<TEntity>();

            var conversionFunc = overloadDefaultConversion ?? _conversionFunc;

            while (await sqlDataReader.ReadAsync(cancellationToken))
            {
                var rowObjects = new List<object>();

                for (var i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    rowObjects.Add(sqlDataReader[i]);
                }

                result.Add(conversionFunc.Invoke(rowObjects));
            }

            await _sqlConnection.CloseAsync();

            return result;
        }
    }
}
