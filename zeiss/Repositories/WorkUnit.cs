using zeiss.DBContext;

namespace zeiss.Repositories
{
    public class WorkUnit : IWorkUnit, IDisposable
    {
        private readonly ZeissContext _dbContext;

        public WorkUnit(ZeissContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
    }
}
