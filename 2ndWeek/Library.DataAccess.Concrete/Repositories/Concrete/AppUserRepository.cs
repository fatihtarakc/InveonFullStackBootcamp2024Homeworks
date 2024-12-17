namespace Library.DataAccess.Concrete.Repositories.Concrete
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(LibraryDbContext db) : base(db) { }

        public async Task<AppUser> IncludeGetFirstOrDefaultAsync(Expression<Func<AppUser, bool>> expression, Expression<Func<AppUser, object>> include, bool tracking = true) =>
            await GetAllByStatusIsNotDeletedByTracking(tracking).Include(include).FirstOrDefaultAsync(expression);
    }
}