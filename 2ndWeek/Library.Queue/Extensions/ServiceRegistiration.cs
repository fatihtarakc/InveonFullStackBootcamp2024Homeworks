namespace Library.Queue.Extensions
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddQueueServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IObjectConvertFormatService, ObjectConvertFormatService>();
            services.AddScoped<IRabbitmqConsumerService, RabbitmqConsumerService>();
            services.AddScoped<IRabbitmqPublisherService, RabbitmqPublisherService>();
            services.AddScoped<IRabbitmqService, RabbitmqService>(serviceProvider => 
            {
                var iOptionsConnectionOptions = serviceProvider.GetRequiredService<IOptions<ConnectionOptions>>();
                //var connectionOptions = configuration.GetSection(ConnectionOptions.Connections).Get<ConnectionOptions>();

                var rabbitmq = new RabbitmqService(iOptionsConnectionOptions);
                rabbitmq.CreateChannelAsync().Wait();
                return rabbitmq;
            });

            services.Configure<ConnectionOptions>
                (configuration.GetSection(ConnectionOptions.Connections));

            return services;
        }
    }
}