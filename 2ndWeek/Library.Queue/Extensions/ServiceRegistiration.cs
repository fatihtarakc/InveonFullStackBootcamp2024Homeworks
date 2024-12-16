namespace Library.Queue.Extensions
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddQueueServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services;
        }
    }
}