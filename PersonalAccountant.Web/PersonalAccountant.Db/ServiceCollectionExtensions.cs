using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalAccountant.Db.Contexts;

namespace PersonalAccountant.Db;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds the PersonalAccountantContext and related repositories to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="connectionString">The connection string.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddPersonalAccountantRepositories(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<PersonalAccountantContext>(options =>
		{
			options.UseSqlServer(connectionString);
		});
		return services;
	}
}