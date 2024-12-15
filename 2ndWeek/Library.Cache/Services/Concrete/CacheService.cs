namespace Library.Cache.Services.Concrete
{
    public class CacheService<Entity> : ICacheService<Entity> where Entity : AuditableBaseEntity
    {
        private readonly IDistributedCache distributedCache;
        public CacheService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<Entity> GetCacheAsync(string cacheKey) =>
            JsonSerializer.Deserialize<Entity>(await distributedCache.GetStringAsync(cacheKey));

        public async Task SetCacheAsync(string cacheKey, Entity entity, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };

            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entity), options);
        }
    }
}