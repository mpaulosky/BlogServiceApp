// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : FixtureExtensions.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.Fixtures;

[ExcludeFromCodeCoverage]
public static class FixtureExtensions
{
	public static IWebHostBuilder UseUniqueDb(this IWebHostBuilder builder, string connection) =>
		builder.ConfigureAppConfiguration(configuration =>
		{
			Guid uniqueId = Guid.NewGuid();
			// Add connection section to the configuration
			var testConfiguration = new Dictionary<string, string>
			{
				{ "MongoDbSettings:ConnectionStrings", connection },
				{ "MongoDbSettings:DatabaseName", $"BlogService{uniqueId:N}" }
			};

			configuration.AddInMemoryCollection(testConfiguration);
		});

	public static void CleanupServices(this IServiceCollection services)
	{
		ServiceDescriptor dbConnectionDescriptor = services.SingleOrDefault(
			d => d.ServiceType == typeof(IMongoDbContextFactory));
		while (dbConnectionDescriptor != null)
		{
			services.Remove(dbConnectionDescriptor);
			dbConnectionDescriptor = services.SingleOrDefault(
				d => d.ServiceType == typeof(IMongoDbContextFactory));
		}

		ServiceDescriptor dbSettings = services.SingleOrDefault(
			d => d.ServiceType == typeof(IDatabaseSettings));
		while (dbSettings != null)
		{
			services.Remove(dbSettings);
			dbSettings = services.SingleOrDefault(
				d => d.ServiceType == typeof(IDatabaseSettings));
		}
	}

	public static void ReplaceRemovedServices(this IServiceCollection services, IDatabaseSettings dbSettings)
	{
		services.AddSingleton(_ => dbSettings);

		services.AddSingleton<IMongoDbContextFactory>(_ => new MongoDbContextFactory(dbSettings));
	}
}
