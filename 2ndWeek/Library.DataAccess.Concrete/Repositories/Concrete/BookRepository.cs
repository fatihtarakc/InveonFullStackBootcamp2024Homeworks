namespace Library.DataAccess.Concrete.Repositories.Concrete
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext db) : base(db) { }

        public async Task<Book> IncludeGetFirstOrDefaultAsync(Expression<Func<Book, bool>> expression, Expression<Func<Book, object>> include, bool tracking = true) =>
            await GetAllByStatusIsNotDeletedByTracking(tracking).Include(include).FirstOrDefaultAsync(expression);

        public async Task<IEnumerable<Book>> IncludeGetAllWhereAsync(Expression<Func<Book, bool>> expression, Expression<Func<Book, object>> include, Expression<Func<Book, object>> orderby, bool tracking = true) =>
            await GetAllByStatusIsNotDeletedByTracking(tracking).Include(include).Where(expression).OrderBy(orderby).ToListAsync();
    }
}