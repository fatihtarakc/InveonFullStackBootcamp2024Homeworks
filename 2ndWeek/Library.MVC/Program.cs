using Library.Business.Extensions;
using Library.DataAccess.Concrete.Extensions;
using Library.DataAccess.Extensions;
using Library.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddDataAccessConcreteServices(builder.Configuration);
builder.Services.AddMvcServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
