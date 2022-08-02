using System.Linq.Expressions;

namespace zeiss.Repositories
{
    public interface IAsyncRepository<TEntity, TId> where TEntity : class
    {
        /// <summary>
        /// Provides more flexible query capability such as to include related entities.
        /// <remarks>
        /// however, it is recommended to use available APIs from <IAsyncRepository></IAsyncRepository> for performance consideration.
        /// </remarks>
        /// </summary>
        public IQueryable<TEntity> NoTrackingQueryable { get; }

        // query
        Task<IList<TEntity>> All();
        Task<IList<TEntity>> All(string[] propertiesToInclude);
        Task<IList<TEntity>> Where(Expression<Func<TEntity, bool>> filter);
        Task<IList<TEntity>> Where(Expression<Func<TEntity, bool>> filter, string[] propertiesToInclude);

        // count
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

        // get single item
        Task<TEntity> GetByIdAsync(TId id);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, string[] propertiesToInclude);

        // get some or all items
        Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter);
        Task<IList<TEntity>> GetAllAsync();

        // add
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entities);

        // update
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        // delete
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
    }
}
