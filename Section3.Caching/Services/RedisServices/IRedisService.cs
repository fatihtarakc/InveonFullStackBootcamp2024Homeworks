using StackExchange.Redis;

namespace Section3.Caching.Services.RedisServices
{
    public interface IRedisService
    {
        public void Connect();
        public IDatabase GetDatabase();
    }
}