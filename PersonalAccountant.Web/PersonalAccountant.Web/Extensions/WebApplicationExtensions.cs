using PersonalAccountant.Common.Options;

namespace PersonalAccountant.Web.Extensions
{
	public static class WebApplicationExtensions
	{
		/// <summary>
		/// Configures the application to use localization with the specified cultures.
		/// </summary>
		/// <param name="app">The application.</param>
		/// <returns>The application.</returns>
		public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
		{
			var localizationOptions = app.ApplicationServices.GetRequiredService<LocalizationOptions>();

			var supportedCultures = localizationOptions.AvailableCultures
				.Select(x => x.CultureCode)
				.ToArray();
			var requestLocalizationOptions = new RequestLocalizationOptions()
				.SetDefaultCulture(localizationOptions.DefaultCulture)
				.AddSupportedCultures(supportedCultures)
				.AddSupportedUICultures(supportedCultures);

			app.UseRequestLocalization(requestLocalizationOptions);

			return app;
		}
	}
}
