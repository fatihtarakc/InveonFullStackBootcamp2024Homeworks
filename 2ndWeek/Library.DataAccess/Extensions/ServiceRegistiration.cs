namespace Library.DataAccess.Extensions
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString(LibraryDbContext.ConnectionName);
            services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentityCore<AppUser>().AddRoles<AppRole>().AddEntityFrameworkStores<LibraryDbContext>();
            return services;
        }
    }
}