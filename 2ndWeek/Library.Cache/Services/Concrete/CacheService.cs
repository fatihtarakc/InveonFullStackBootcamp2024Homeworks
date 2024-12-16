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
            var jsonData = await distributedCache.GetStringAsync(cacheKey);
            if (jsonData is null) return new ErrorDataResult<Entity>(Message.Redis_Cache_Entity_Was_Not_Found);

            return new SuccessDataResult<Entity>(JsonSerializer.Deserialize<Entity>(jsonData), Message.Redis_Cache_Entity_Was_Found);
        }

        public async Task<IResult> SetCacheAsync(string cacheKey, Entity entity, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };

            try
            {
                await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entity), options);
                return new SuccessResult(Message.Redis_Cache_Entity_Was_Added);
            }
            catch (Exception exception)
            {
                return new ErrorResult($"{Message.Redis_Cache_Entity_Was_Not_Added} : {exception.Message}");
            }
        }
    }
}