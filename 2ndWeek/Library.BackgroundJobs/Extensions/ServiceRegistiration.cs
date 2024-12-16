namespace Library.BackgroundJobs.Extensions
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddBackgroundJobsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionOptions>
                (configuration.GetSection(ConnectionOptions.Connections));

            var serviceProvider = services.BuildServiceProvider();
            var connectionOptions = serviceProvider.GetRequiredService<IOptions<ConnectionOptions>>().Value;
            //var connectionOptions = configuration.GetSection(ConnectionOptions.Connections).Get<ConnectionOptions>();
            services.AddHangfire(configuration =>
            {
                configuration.UseSqlServerStorage(connectionOptions.Hangfire);
            });
            services.AddHangfireServer();

            //FireAndForgetJobs.SendEmailJob();
            services.AddScoped<IJobSchedulerService, JobSchedulerService>(serviceProvider => 
            {
                var jobScheculerService = serviceProvider.GetRequiredService<JobSchedulerService>();
                jobScheculerService.ScheduleJobs();
                return jobScheculerService;
            });

            return services;
        }

        public static IApplicationBuilder AddBackgroundJobsUseHangfireDashboardWithPathApplicationBuilder(this IApplicationBuilder applicationBuilder, string pathMatch = "/hangfire")
        {
            applicationBuilder.UseHangfireDashboard(pathMatch, new DashboardOptions
            {
                DashboardTitle = "Library Hangfire DashBoard",
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            return applicationBuilder;
        }
    }
}