using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using PersonalAccountant.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

services.AddControllersWithViews();
services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
		})
	.AddCookie(IdentityConstants.ExternalScheme)
	.AddGoogle(options =>
	{
		options.ClientId = "1089856459789-88nm7u045cipeh35um51rd0nh4jp4boj.apps.googleusercontent.com";
		options.ClientSecret = "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";
		options.SignInScheme = IdentityConstants.ExternalScheme;
	});
services.AddPersonalAccountantRepositories(connectionString);
services.AddHttpContextAccessor();

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

app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}")
	.RequireAuthorization();

app.Run();
