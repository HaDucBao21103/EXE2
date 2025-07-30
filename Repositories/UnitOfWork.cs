using Repositories.Context;
using Repositories.Interfaces;

namespace Repositories
{
    public class UnitOfWork(EXE201_SU25DbContext context): IUnitOfWork
    {
        private bool disposed = false;
        private readonly EXE201_SU25DbContext _context = context;

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void RollBack()
        {
            _context.Database.RollbackTransaction();
            //_context.ChangeTracker.Clear();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }
    }
}
