// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : FixtureExtensions.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI.Tests.Playwright.Fixtures;

[ExcludeFromCodeCoverage]
public static class FixtureExtensions
{
	public static void AddNewMongoDbSettingsSectionToConfig(this IHostBuilder builder, IDatabaseSettings settings)
	{
		builder.ConfigureAppConfiguration(configuration =>
		{
			// Add connection section to the configuration

			var testConfiguration = new Dictionary<string, string>
			{
				{ "MongoDbSettings:ConnectionStrings", settings.ConnectionStrings },
				{ "MongoDbSettings:DatabaseName", settings.DatabaseName }
			};

			configuration.AddInMemoryCollection(testConfiguration!);
		});
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

	public static IMongoDbContextFactory GetDatabaseContext(this IServiceProvider serviceProvider)
	{
		return serviceProvider.GetRequiredService<IMongoDbContextFactory>();
	}
}
