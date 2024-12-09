
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Section3.Caching.Services.RedisCacheServices
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache distributedCache;
        public RedisCacheService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task SetCacheAsync<T>(string key, T value, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions 
            { 
                AbsoluteExpirationRelativeToNow = expiration 
            }; 
            var serializedData = JsonSerializer.Serialize(value);
            await distributedCache.SetStringAsync(key, serializedData, options);
        }

        public async Task<T> GetCacheAsync<T>(string key)
        {
            var serializedData = await distributedCache.GetStringAsync(key);
            if (serializedData is null) return default;

            return JsonSerializer.Deserialize<T>(serializedData);
        }
    }
}