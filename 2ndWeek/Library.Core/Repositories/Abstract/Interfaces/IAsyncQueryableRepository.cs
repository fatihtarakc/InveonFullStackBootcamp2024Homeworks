namespace Library.Core.Repositories.Abstract.Interfaces
{
    public interface IAsyncQueryableRepository<Entity> where Entity : AuditableBaseEntity
    {
        Task<Entity> GetById(Guid entityId, bool tracking = true);
        Task<bool> ExistsAsync(Expression<Func<Entity, bool>> expression, bool tracking = true);
        Task<Entity> GetFirstOrDefaultAsync(Expression<Func<Entity, bool>> expression, bool tracking = true);
        Task<IEnumerable<Entity>> GetAllWhereAsync(Expression<Func<Entity, bool>> expression, bool tracking = true); 
        Task<IEnumerable<Entity>> GetAllAsync(bool tracking = true);
    }
}