namespace Library.DataAccess.Abstract.Repositories.Abstract
{
    public interface IBookRepository :
        IAsyncAddableRepository<Book>, IAsyncDeletableRepository<Book>, IAsyncUpdatableRepository<Book>,
        IAsyncQueryableRepository<Book>, IAsyncOrderableRepository<Book>
    {
        Task<Book> IncludeGetFirstOrDefaultAsync(Expression<Func<Book, bool>> expression, Expression<Func<Book, object>> include, bool tracking = true);

        //Task<Book> IncludeGetFirstOrDefaultAsync(Expression<Func<Book, bool>> expression, Expression<Func<Book, object>> include, Expression<Func<object, object>> thenInclude, bool tracking = true);

        Task<IEnumerable<Book>> IncludeGetAllWhereAsync(Expression<Func<Book, bool>> expression, Expression<Func<Book, object>> include, Expression<Func<Book, object>> orderby, bool tracking = true);

        //Task<IEnumerable<Book>> IncludeGetAllWhereAsync(Expression<Func<Book, bool>> expression, Expression<Func<Book, object>> include, Expression<Func<object, object>> thenInclude, Expression<Func<Book, object>> orderby, bool tracking = true);
    }
}