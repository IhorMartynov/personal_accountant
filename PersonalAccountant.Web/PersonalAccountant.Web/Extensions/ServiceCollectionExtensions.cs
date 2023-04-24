using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using PersonalAccountant.Common.Options;

namespace PersonalAccountant.Web.Extensions;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds the application options to the <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="configuration">The configuration manager.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	public static IServiceCollection AddApplicationOptions(this IServiceCollection services,
		ConfigurationManager configuration)
	{
		var localizationOptions = configuration.GetRequiredSection("Localization")
			.Get<LocalizationOptions>()!;

		var authenticationOptions = configuration.GetRequiredSection("Authentication")
			.Get<AuthenticationOptions>()!;

		services.AddSingleton(localizationOptions);
		services.AddSingleton(authenticationOptions);

		return services;
	}

	/// <summary>
	/// Adds authentication services to the specified <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
	{
		var sp = services.BuildServiceProvider();
		var authenticationOptions = sp.GetRequiredService<AuthenticationOptions>();

		services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
			})
			.AddCookie(IdentityConstants.ExternalScheme)
			.AddGoogle(options =>
			{
				options.ClientId = authenticationOptions.Google.ClientId;
				options.ClientSecret = authenticationOptions.Google.ClientSecret;
				options.SignInScheme = IdentityConstants.ExternalScheme;
			});

		return services;
	}
}