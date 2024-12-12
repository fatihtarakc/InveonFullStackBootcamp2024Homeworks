namespace Library.Core.Repositories.Abstract.Interfaces
{
    public interface IAsyncUpdatableRepository<Entity> where Entity : AuditableBaseEntity
    {
        Task<Entity> UpdateAsync(Entity entity);
    }
}