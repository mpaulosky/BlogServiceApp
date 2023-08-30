﻿using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlogService.UI.Tests.Playwright;

[ExcludeFromCodeCoverage]
public static class MongoDbServiceExtensions
{
	private static void UpdateDatabaseServices(this IServiceCollection services, IDatabaseSettings settings)
	{
		services.RemoveAll<IDatabaseSettings>();
		services.RemoveAll<IMongoDbContextFactory>();

		services.AddSingleton(_ => settings);

		services.AddSingleton<IMongoDbContextFactory>(_ => new MongoDbContextFactory(settings));
	}

	public static IHostBuilder UpdateDatabaseServices(this IHostBuilder builder, IDatabaseSettings settings) =>
		builder.ConfigureServices(services => services.UpdateDatabaseServices(settings));

	public static IWebHostBuilder UpdateDatabaseServices(this IWebHostBuilder builder, IDatabaseSettings settings) =>
		builder.ConfigureServices(services => services.UpdateDatabaseServices(settings));
}