// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     FixtureExtensions.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI.Tests.Playwright.Fixtures;

public static class FixtureExtensions
{
	public static IHostBuilder UseUniqueDb(this IHostBuilder builder, Guid id) =>
		builder.ConfigureAppConfiguration(configuration =>
		{
			var testConfiguration = new Dictionary<string, string>
			{
				{ "ConnectionStrings:SecurityContextConnection", $"Data Source=TagzApp.Web.{id:N}.db" }
			};
			configuration.AddInMemoryCollection(testConfiguration);
		});

	public static async Task CleanUpDbFilesAsync(this Guid id, ILogger logger = null!)
	{
		logger ??= Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
		// The host should have shutdown here so we can delete the test database files
		await Task.Delay(50);
		var dbFiles = Directory.GetFiles(".", $"TagzApp.Web.{id:N}.db*");
		foreach (var dbFile in dbFiles)
		{
			try
			{
				logger.LogInformation("Removing test database file {File}", dbFile);
				File.Delete(dbFile);
			}
			catch (Exception e)
			{
				logger.LogWarning("Could not remove test database file {File}: {Reason}", dbFile, e.Message);
			}
		}
	}

	/// <summary>
	///   Add the file provided from the test project to the host app configuration
	/// </summary>
	/// <param name="builder">The IHostBuilder</param>
	/// <param name="fileName">The filename or null (defaults to appsettings.Test.json)</param>
	/// <returns>Returns the IHostBuilder to allow chaining</returns>
	public static IHostBuilder AddTestConfiguration(this IHostBuilder builder, string? fileName = null)
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

	public static void SetMongoDbContainer(this IServiceProvider builder, MongoDbContainer mongoDbContainer) =>
		builder..ConfigureServices(services => services.CreateDatabase(mongoDbContainer));

	private static void CreateDatabase(this IServiceCollection services, MongoDbContainer mongoDbContainer)
	{

	}

	public static IHostBuilder UseOnlyTestContainer(this IHostBuilder builder) =>
		builder.ConfigureServices(services => services.UseOnlyTestContainerConnections());

	private static void UseOnlyTestContainerConnections(this IServiceCollection services)
	{
		ServiceDescriptor? dbConnectionDescriptor =
			services.SingleOrDefault(d => d.ServiceType == typeof(IMongoDbContextFactory));

		services.Remove(dbConnectionDescriptor!);

		ServiceDescriptor? dbSettings =
			services.SingleOrDefault(d => d.ServiceType == typeof(IDatabaseSettings));

		services.Remove(dbSettings!);
	}
}
