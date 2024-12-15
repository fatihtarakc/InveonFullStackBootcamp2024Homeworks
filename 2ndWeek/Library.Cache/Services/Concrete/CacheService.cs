namespace Library.Cache.Services.Concrete
{
    public class CacheService<Entity> : ICacheService<Entity> where Entity : AuditableBaseEntity
    {
        private readonly IDistributedCache distributedCache;
        public CacheService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<IDataResult<Entity>> GetCacheAsync(string cacheKey)
        {
            var entity = JsonSerializer.Deserialize<Entity>(await distributedCache.GetStringAsync(cacheKey));
            if (entity is null) return new ErrorDataResult<Entity>(Message.Redis_Cache_Entity_Not_Found);

            return new SuccessDataResult<Entity>(entity, Message.Redis_Cache_Entity_Not_Found);
        }

        public async Task<IResult> SetCacheAsync(string cacheKey, Entity entity, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };

            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entity), options);
            return new Result()
        }
    }
}