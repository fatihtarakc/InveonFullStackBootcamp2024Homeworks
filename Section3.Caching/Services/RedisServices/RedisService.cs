using StackExchange.Redis;

namespace Section3.Caching.Services.RedisServices
{
    public class RedisService : IRedisService
    {
        public RedisService(string host, int port)
        {
        }

        private string host;
        private int port;
        private ConnectionMultiplexer connectionMultiplexer;

        public void Connect() =>
            connectionMultiplexer = ConnectionMultiplexer.Connect($"{host}:{port}");

        public IDatabase GetDatabase() =>
            connectionMultiplexer.GetDatabase(0);
    }
}