using Microsoft.Extensions.Options;
using Section3.Caching.Services.RedisCacheServices;
using Section3.Caching.Services.RedisServices;

namespace Section3.Caching.Extensions
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<Options.RedisOptions>
                (configuration.GetSection(Options.RedisOptions.Redis));

            services.AddScoped<IRedisCacheService, RedisCacheService>();

            services.AddScoped<IRedisService, RedisService>(serviceProvider => 
            {
                var redisOptions = serviceProvider.GetRequiredService<IOptions<Options.RedisOptions>>().Value;
                var redis = new RedisService(redisOptions.Host, redisOptions.Port);
                redis.Connect();
                return redis;
            });

            return services;
        }
    }
}