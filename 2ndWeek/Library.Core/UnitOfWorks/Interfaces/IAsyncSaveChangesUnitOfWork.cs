namespace Library.Core.UnitOfWorks.Interfaces
{
    public interface IAsyncSaveChangesUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}