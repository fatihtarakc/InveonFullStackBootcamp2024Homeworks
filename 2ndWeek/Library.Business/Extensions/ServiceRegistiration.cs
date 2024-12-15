namespace Library.Business.Extensions
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<EmailOptions>
                (configuration.GetSection(EmailOptions.EmailConfiguraiton));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}