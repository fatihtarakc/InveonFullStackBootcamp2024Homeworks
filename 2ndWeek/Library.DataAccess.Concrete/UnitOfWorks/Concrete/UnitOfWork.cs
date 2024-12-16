namespace Library.DataAccess.Concrete.UnitOfWorks.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly LibraryDbContext db;
        public UnitOfWork(LibraryDbContext db)
        {
            this.db = db;
        }

        #region IAsyncTransactionUnitOfWork
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
            db.Database.BeginTransactionAsync(cancellationToken);

        public async Task<IExecutionStrategy> CreateExecutionStrategy() =>
            await Task.FromResult(db.Database.CreateExecutionStrategy());
        #endregion

        #region IAsyncSaveChangesUnitOfWork
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await db.SaveChangesAsync(cancellationToken);
        #endregion

        #region IDispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) db.Dispose();

                disposed = true;
            }
        }
        #endregion

        #region IDisposeAsync
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) await db.DisposeAsync();

                disposed = true;
            }
        }
        #endregion
    }
}