namespace Library.Core.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork :
        IAsyncSaveChangesUnitOfWork, IAsyncTransactionUnitOfWork,
        IDisposable, IAsyncDisposable { }
}