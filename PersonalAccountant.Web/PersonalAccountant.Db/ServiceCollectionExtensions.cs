using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PersonalAccountant.Db.Contracts.Repositories;
using PersonalAccountant.Db.Mappers;
using PersonalAccountant.Db.Repositories;

namespace PersonalAccountant.Db;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds MongoDB repositories to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="connectionString">The connection string.</param>
	/// <param name="databaseName">The name of the database.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddMongoDbRepositories(this IServiceCollection services,
		string connectionString,
		string databaseName)
	{
		services.AddSingleton(new MongoClient(connectionString));

		services.AddTransient(sp =>
		{
			var client = sp.GetRequiredService<MongoClient>();
			return client.GetDatabase(databaseName);
		});

		services.AddSingleton<IUserSettingsDtoMapper, UserSettingsDtoMapper>()
			.AddSingleton<IUserSettingsMapper, UserSettingsMapper>();

		services.AddTransient<IUserSettingsRepository, UserSettingsRepository>();

		return services;
	}
}