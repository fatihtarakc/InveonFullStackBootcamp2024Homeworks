namespace Library.Core.UnitOfWork.Interfaces
{
    public interface IAsyncUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}