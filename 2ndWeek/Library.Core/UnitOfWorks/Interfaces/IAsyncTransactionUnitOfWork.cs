﻿namespace Library.Core.UnitOfWorks.Interfaces
{
    public interface IAsyncTransactionUnitOfWork
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<IExecutionStrategy> CreateExecutionStrategy();
    }
}