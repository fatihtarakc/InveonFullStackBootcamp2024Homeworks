namespace Library.DataAccess.Abstract.Repositories.Abstract
{
    public interface IUserRepository :
        IAsyncAddableRepository<User>, IAsyncDeletableRepository<User>, IAsyncUpdatableRepository<User>,
        IAsyncQueryableRepository<User>, IAsyncOrderableRepository<User>
    {
    }
}