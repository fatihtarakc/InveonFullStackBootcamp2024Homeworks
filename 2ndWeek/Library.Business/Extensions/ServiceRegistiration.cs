namespace Library.Business.Extensions
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IEmailService, EmailService>();

            services.Configure<EmailOptions>
                (configuration.GetSection(EmailOptions.EmailConfiguraiton));

            return services;
        }
    }
}