namespace Library.DataAccess.Abstract.Repositories.Abstract
{
    public interface IBookRepository :
        IAsyncAddableRepository<Book>, IAsyncDeletableRepository<Book>, IAsyncUpdatableRepository<Book>,
        IAsyncQueryableRepository<Book>, IAsyncOrderableRepository<Book>
    {
    }
}