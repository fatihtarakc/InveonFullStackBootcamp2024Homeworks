namespace Library.Core.Repositories.Abstract.Interfaces
{
    public interface IAsyncDeletableRepository<Entity> where Entity : AuditableBaseEntity
    {
        Task DeleteAsync(Entity entity);
    }
}