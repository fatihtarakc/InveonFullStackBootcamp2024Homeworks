namespace Library.MVC.Extensions
{
    public static class ServiceRegistiration
    {
        public static WebApplicationBuilder AddLoggingWebApplicationBuilder(this WebApplicationBuilder builder)
        {
            builder.Logging
                .AddConfiguration(builder.Configuration.GetSection("Logging"))
                .ClearProviders()
                .AddConsole()
                .AddDebug();

            return builder;
        }

        public static IServiceCollection AddMvcServices(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = "LibraryCookie";
                options.ExpireTimeSpan = TimeSpan.FromDays(60);
                options.LoginPath = "/Account/SignIn";
                options.SlidingExpiration = true;
            });
            services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddLocalization();
            services.AddNotyf(configure =>
            {
                configure.DurationInSeconds = 5;
                configure.HasRippleEffect = true;
                configure.IsDismissable = true;
                configure.Position = NotyfPosition.BottomRight;
            });
            services.AddRazorPages();

            return services;
        }

        public static IApplicationBuilder UseNotifyUseRequestLocalizationApplicationBuilder(this IApplicationBuilder applicationBuilder)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"), new CultureInfo("tr-TR")
            };

            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                DefaultRequestCulture = new RequestCulture("tr-TR"),
                ApplyCurrentCultureToResponseHeaders = true
            };

            applicationBuilder.UseNotyf();
            applicationBuilder.UseRequestLocalization(requestLocalizationOptions);

            return applicationBuilder;
        }
    }
}