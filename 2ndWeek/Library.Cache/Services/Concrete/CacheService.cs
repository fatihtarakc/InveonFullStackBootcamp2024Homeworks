namespace Library.Cache.Services.Concrete
{
    public class CacheService<Entity> : ICacheService<Entity> where Entity : AuditableBaseEntity
    {
        private readonly IDistributedCache distributedCache;
        private readonly IStringLocalizer<MessageResources> stringLocalizer;
        private readonly ILogger<CacheService<Entity>> logger;
        public CacheService(IDistributedCache distributedCache, IStringLocalizer<MessageResources> stringLocalizer, ILogger<CacheService<Entity>> logger)
        {
            this.distributedCache = distributedCache;
            this.stringLocalizer = stringLocalizer;
            this.logger = logger;
        }

        public async Task<IDataResult<Entity>> GetCacheAsync(string cacheKey)
        {
            var jsonData = await distributedCache.GetStringAsync(cacheKey);
            if (jsonData is null) return new ErrorDataResult<Entity>(stringLocalizer[Messages.Redis_Cache_Entity_Was_Not_Found]);

            return new SuccessDataResult<Entity>(JsonSerializer.Deserialize<Entity>(jsonData), stringLocalizer[Messages.Redis_Cache_Entity_Was_Found]);
        }

        public async Task<IResult> SetCacheAsync(string cacheKey, Entity entity, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };

            try
            {
                await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entity), options);
                return new SuccessResult(stringLocalizer[Messages.Redis_Cache_Entity_Was_Added]);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return new ErrorResult($"{stringLocalizer[Messages.Redis_Cache_Entity_Was_Not_Added]} : {exception.Message}");
            }
        }
    }
}