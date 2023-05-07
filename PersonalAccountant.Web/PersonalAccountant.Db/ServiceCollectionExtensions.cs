using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PersonalAccountant.Db.Contracts.Repositories;
using PersonalAccountant.Db.Mappers;
using PersonalAccountant.Db.Repositories;
using PersonalAccountant.Db.Services;

[assembly:InternalsVisibleTo("PersonalAccountant.Db.Tests")]

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

        services.AddHostedService<ConfigureMongoDbIndexesService>();

		services.AddSingleton<IUserSettingsDtoMapper, UserSettingsDtoMapper>()
			.AddSingleton<IUserSettingsMapper, UserSettingsMapper>()
            .AddSingleton<IPublicEnterpriseDtoMapper, PublicEnterpriseDtoMapper>()
            .AddSingleton<IPublicEnterpriseMapper, PublicEnterpriseMapper>();

		services.AddTransient<IUserSettingsRepository, UserSettingsRepository>()
            .AddTransient<IPublicEnterprisesRepository, PublicEnterprisesRepository>();

		return services;
	}
}