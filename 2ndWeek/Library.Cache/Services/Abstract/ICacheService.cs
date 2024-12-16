namespace Library.Cache.Services.Abstract
{
    public interface ICacheService<Entity> where Entity : AuditableBaseEntity
    {
        Task<IDataResult<Entity>> GetCacheAsync(string cacheKey);

        Task<IResult> SetCacheAsync(string cacheKey, Entity entity, TimeSpan expiration);
    }
}