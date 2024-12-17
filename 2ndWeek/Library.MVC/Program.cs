var builder = WebApplication.CreateBuilder(args);

builder.AddMvcLoggingWebApplicationBuilder();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddDataAccessConcreteServices(builder.Configuration);
builder.Services.AddCacheServices(builder.Configuration);
builder.Services.AddBusinessConcreteServices(builder.Configuration);
builder.Services.AddQueueServices(builder.Configuration);
builder.Services.AddBackgroundJobsServices(builder.Configuration);
builder.Services.AddMvcServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.AddMvcUseNotifyUseRequestLocalizationApplicationBuilder();
app.AddBackgroundJobsUseHangfireDashboardWithPathApplicationBuilder("/hangfire");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Account}/{action=SignIn}/{id?}");
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();