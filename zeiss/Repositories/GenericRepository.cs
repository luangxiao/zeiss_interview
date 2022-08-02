using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using zeiss.DBContext;

namespace zeiss.Repositories
{
    public class GenericRepository<TEntity, TId> : IAsyncRepository<TEntity, TId> where TEntity : class
    {
        protected readonly ZeissContext _dbContext;

        public IQueryable<TEntity> NoTrackingQueryable => _dbContext.Set<TEntity>().AsNoTracking();

        public GenericRepository(ZeissContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual Task<IList<TEntity>> All()
        {
            return All(null);
        }

        public virtual async Task<IList<TEntity>> All(string[] propertiesToInclude)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();

            if (propertiesToInclude != null)
            {
                foreach (var property in propertiesToInclude.Where(p => !string.IsNullOrWhiteSpace(p)))
                {
                    query = query.Include(property);
                }
            }

            return await query.ToListAsync();
        }

        public virtual Task<IList<TEntity>> Where(Expression<Func<TEntity, bool>> filter)
        {
            return Where(filter, null);
        }

        public virtual async Task<IList<TEntity>> Where(Expression<Func<TEntity, bool>> filter, string[] propertiesToInclude)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (propertiesToInclude != null)
            {
                foreach (var property in propertiesToInclude.Where(p => !string.IsNullOrWhiteSpace(p)))
                {
                    query = query.Include(property);
                }
            }

            return await query.ToListAsync();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(filter);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbContext.Set<TEntity>().CountAsync(filter);
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await GetFirstOrDefaultAsync(filter, null);
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, string[] propertiesToInclude)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (propertiesToInclude != null)
            {
                foreach (var property in propertiesToInclude.Where(p => !string.IsNullOrWhiteSpace(p)))
                {
                    query = query.Include(property);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbContext.Set<TEntity>().Where(filter).AsNoTracking().ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task AddAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return;
            }

            foreach (var entity in entities)
            {
                _dbContext.Set<TEntity>().Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Set<TEntity>().Attach(entity);
            }

            _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return;
            }

            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
