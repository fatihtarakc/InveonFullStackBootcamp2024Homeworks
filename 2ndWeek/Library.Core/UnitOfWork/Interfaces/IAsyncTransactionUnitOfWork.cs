namespace Library.Core.UnitOfWork.Interfaces
{
    public interface IAsyncTransactionUnitOfWork
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<IExecutionStrategy> CreateExecutionStrategy();
    }
}