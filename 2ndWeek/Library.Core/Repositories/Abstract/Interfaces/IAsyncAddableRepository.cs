namespace Library.Core.Repositories.Abstract.Interfaces
{
    public interface IAsyncAddableRepository<Entity> where Entity : AuditableBaseEntity
    {
        Task<Entity> AddAsync(Entity entity);
    }
}