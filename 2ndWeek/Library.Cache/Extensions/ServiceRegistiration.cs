﻿namespace Library.Cache.Extensions
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionOptions>
                (configuration.GetSection(ConnectionOptions.Connections));

            var serviceProvider = services.BuildServiceProvider();
            var connectionOptions = serviceProvider.GetRequiredService<IOptions<ConnectionOptions>>().Value;
            //var connectionOptions = configuration.GetSection(ConnectionOptions.Connections).Get<ConnectionOptions>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString(connectionOptions.RedisConnectionString);
                options.InstanceName = "RedisInstance";
            });
            return services;
        }
    }
}