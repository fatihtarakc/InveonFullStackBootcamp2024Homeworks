var builder = WebApplication.CreateBuilder(args);

builder.AddMvcLoggingWebApplicationBuilder();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddBackgroundJobsServices(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddCacheServices(builder.Configuration);
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddDataAccessConcreteServices(builder.Configuration);
builder.Services.AddMvcServices();
builder.Services.AddQueueServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.AddMvcUseNotifyUseRequestLocalizationApplicationBuilder();
app.AddBackgroundJobsUseHangfireDashboardWithPathApplicationBuilder();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();