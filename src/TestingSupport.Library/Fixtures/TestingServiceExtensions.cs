using BlogService.Library.Contracts;
using BlogService.Library.DataAccess;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TestingSupport.Library.Fixtures;

public static class TestingServiceExtensions
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

	private static void AddTestInMemoryMongoDbSection(this IConfigurationBuilder configuration,
		IDatabaseSettings settings)
	{
		// Add connection section to the configuration

		var testConfiguration = new Dictionary<string, string>
		{
			{ "MongoDbSettings:ConnectionStrings", settings.ConnectionStrings },
			{ "MongoDbSettings:DatabaseName", settings.DatabaseName }
		};

		configuration.AddInMemoryCollection(testConfiguration!);
	}

	public static IHostBuilder AddTestInMemoryMongoDbSection(this IHostBuilder builder, IDatabaseSettings settings) =>
		builder.ConfigureAppConfiguration(configuration => configuration.AddTestInMemoryMongoDbSection(settings));

	public static IWebHostBuilder
		AddTestInMemoryMongoDbSection(this IWebHostBuilder builder, IDatabaseSettings settings) =>
		builder.ConfigureAppConfiguration(configuration => configuration.AddTestInMemoryMongoDbSection(settings));


	/// <summary>
	///   Add the file provided from the test project to the host app configuration
	/// </summary>
	/// <param name="builder">The IHostBuilder</param>
	/// <param name="fileName">The filename or null (defaults to appsettings.Test.json)</param>
	/// <returns>Returns the IHostBuilder to allow chaining</returns>
	public static IHostBuilder AddTestConfiguration(this IHostBuilder builder, string fileName = null)
	{
		var testDirectory = Directory.GetCurrentDirectory();
		builder.ConfigureAppConfiguration(host =>
			host.AddJsonFile(Path.Combine(testDirectory, fileName ?? "appsettings.Test.json"), true));
		return builder;
	}

	/// <summary>
	///   Applies a startup delay based on the configuration parameter TestHostStartDelay. This allows easy adding of a custom
	///   delay on build / test servers.
	/// </summary>
	/// <param name="serviceProvider">The IServiceProvider used to get the IConfiguration</param>
	/// <remarks>The default delay if no value is found is 0 and no delay is applied.</remarks>
	public static async Task ApplyStartUpDelay(this IServiceProvider serviceProvider)
	{
		var config = serviceProvider.GetRequiredService<IConfiguration>();
		if (int.TryParse(config["TestHostStartDelay"] ?? "0", out var delay) && delay != 0)
		{
			await Task.Delay(delay);
		}
	}

	private static IMongoDbContextFactory GetDatabaseContext(this IServiceCollection services)
	{
		using ServiceProvider serviceProvider = services.BuildServiceProvider();

		var context = serviceProvider.GetRequiredService<IMongoDbContextFactory>();

		return context;
	}

	public static IMongoDbContextFactory GetDatabaseContext(this IHostBuilder builder) =>
		(IMongoDbContextFactory)builder.ConfigureServices(services => services.GetDatabaseContext());

	public static IMongoDbContextFactory GetDatabaseContext(this IWebHostBuilder builder) =>
		(IMongoDbContextFactory)builder.ConfigureServices(services => services.GetDatabaseContext());
}
