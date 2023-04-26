using PersonalAccountant.Db;
using PersonalAccountant.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("MongoDB")!;

services.AddControllersWithViews()
	.AddMvcLocalization(options => options.ResourcesPath = "Resources");

services.AddApplicationOptions(builder.Configuration);

services.AddAuthenticationServices();

services.AddMongoDbRepositories(connectionString, "personal-accountant-database");
services.AddHttpContextAccessor();
services.AddDataProtection();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseLocalization();

app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}")
	.RequireAuthorization();

app.Run();
