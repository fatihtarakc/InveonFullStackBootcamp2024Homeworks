namespace Library.DataAccess.Abstract.Repositories.Abstract
{
    public interface IAppUserRepository :
        IAsyncAddableRepository<AppUser>, IAsyncDeletableRepository<AppUser>, IAsyncUpdatableRepository<AppUser>,
        IAsyncQueryableRepository<AppUser>, IAsyncOrderableRepository<AppUser>
    {
        Task<AppUser> IncludeGetFirstOrDefaultAsync(Expression<Func<AppUser, bool>> expression, Expression<Func<AppUser, object>> include, bool tracking = true);

        //Task<AppUser> IncludeGetFirstOrDefaultAsync(Expression<Func<AppUser, bool>> expression, Expression<Func<AppUser, object>> include, Expression<Func<object, object>> thenInclude, bool tracking = true);
    }
}