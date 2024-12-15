namespace Library.Cache.Services.Abstract
{
    public interface ICacheService<Entity> where Entity : AuditableBaseEntity
    {
        Task<Entity> GetCacheAsync(string cacheKey);
        Task SetCacheAsync(string cacheKey, Entity entity, TimeSpan expiration);
    }
}