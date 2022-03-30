using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using DapperPatterns.Common;
using DapperPatterns.Domain;

namespace DapperPatterns.Dapper
{
    public abstract class DapperRepository<TEntity> :
        IGetAll<TEntity>,
        IAddEntity<TEntity>,
        IGetById<TEntity>,
        IDeleteEntity<TEntity>,
        IAddEntities<TEntity>
            where TEntity : IEntity<Guid>
    {
        private readonly SqlConnection _connection;
        private readonly SqlBuilder<TEntity, Guid> _sqlBuilder;

        private readonly Func<SqlConnection, string, DbTransaction, Task<IEnumerable<TEntity>>> _executionFunc;

        protected DapperRepository(SqlConnection connection, SqlBuilder<TEntity, Guid> sqlBuilder, Func<SqlConnection, string, DbTransaction, Task<IEnumerable<TEntity>>> executionFunc)
        {
            _connection = connection;
            _sqlBuilder = sqlBuilder;
            _executionFunc = executionFunc;
        }

        public async Task<List<TEntity>> GetAll()
        {
            var sql = _sqlBuilder.SelectAll().Query;

            var result = await ExecuteQuery(sql);

            return result.ToList();
        }

        public async Task<int> AddEntity(TEntity entity)
        {
            var sql = _sqlBuilder.Insert(entity).Query;

            return await ExecuteCommand(sql);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var sql = _sqlBuilder.GetById(id).Query;

            var result = await ExecuteQuery(sql);

            return result.First();
        }

        public async Task<int> DeleteEntity(TEntity entity)
        {
            var sql = _sqlBuilder.Delete(entity).Query;

            return await ExecuteCommand(sql);
        }

        public async Task<int> AddEntities(IEnumerable<TEntity> entities)
        {
            var sql = _sqlBuilder.InsertMultiple(entities).Query;

            return await ExecuteCommand(sql);
        }

        private async Task<IEnumerable<TEntity>> ExecuteQuery(string sql)
        {
            await _connection.OpenAsync();

            var transaction = await _connection.BeginTransactionAsync();

            IEnumerable<TEntity> result;

            try
            {
                result = await _executionFunc.Invoke(_connection, sql, transaction);

                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();

                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return result;
        }

        private async Task<int> ExecuteCommand(string sql)
        {
            await _connection.OpenAsync();

            var transaction = await _connection.BeginTransactionAsync();

            int result;

            try
            {
                result = await _connection.ExecuteAsync(sql, null, transaction);

                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();

                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return result;
        }
    }

    public abstract class DapperRepository<TFirst, TSecond, TEntity> : DapperRepository<TEntity>
    where TEntity : IEntity<Guid>
    {
        protected DapperRepository(SqlConnection connection, SqlBuilder<TEntity, Guid> sqlBuilder, Func<TFirst, TSecond, TEntity> mappingFunc)
            : base(connection, sqlBuilder, (sqlConnection, sql, transaction) => sqlConnection.QueryAsync(sql, mappingFunc, null, transaction))
        {
        }
    }

    public abstract class DapperRepository<TFirst, TEntity> : DapperRepository<TEntity>
        where TEntity : IEntity<Guid>
    {
        protected DapperRepository(SqlConnection connection, SqlBuilder<TEntity, Guid> sqlBuilder, Func<TFirst, TEntity> mappingFunc) 
            : base(connection, sqlBuilder, async (sqlConnection, sql, transaction) =>
            {
                var result = await sqlConnection.QueryAsync<TFirst>(sql, null, transaction);

                return result.Select(mappingFunc.Invoke).ToList();
            })
        {
        }
    }
}
