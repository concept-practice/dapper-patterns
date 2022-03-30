using DapperPatterns.Common;
using DapperPatterns.Domain;
using Microsoft.EntityFrameworkCore;

namespace DapperPatterns.Dapper
{
    public abstract class EFRepository<TEntity> :
        IGetAll<TEntity>,
        IAddEntity<TEntity>,
        IGetById<TEntity>,
        IDeleteEntity<TEntity>,
        IAddEntities<TEntity>
        where TEntity : class, IEntity<Guid>
    {
        private readonly DbContext _context;
        private readonly Func<IQueryable<TEntity>, IQueryable<TEntity>> _includeFunc;

        protected EFRepository(DbContext context, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc)
        {
            _context = context;
            _includeFunc = includeFunc;
        }

        protected EFRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await IncludeEntities().ToListAsync();
        }

        public async Task<int> AddEntity(TEntity entity)
        {
            await _context.AddAsync(entity);

            return await _context.SaveChangesAsync();
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

        private IQueryable<TEntity> IncludeEntities()
        {
            return _includeFunc == null ? _context.Set<TEntity>() : _includeFunc.Invoke(_context.Set<TEntity>());
        }
    }
}
