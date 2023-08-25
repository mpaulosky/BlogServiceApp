// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     PlaywrightFixture.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI.Tests.Playwright.Fixtures;

/// <summary>
///   WebApplicationFactory that wraps the TestHost in a Kestrel server and provides Playwright and HttpClient testing.
///   This also logs output from the Host under test to Xunit.
///   <p>
///     Credit to <a href="https://github.com/CZEMacLeod">https://github.com/CZEMacLeod</a> for writing this.
///     Functionality is now wrapped in the nuget package C3D.Extensions.Playwright.AspNetCore.Xunit
///   </p>
/// </summary>
[UsedImplicitly]
public class PlaywrightFixture : PlaywrightFixture<IAppMarker>
{
	public override string Environment { get; } = "Development";
	private readonly Guid _uniqueId = Guid.NewGuid();
	private readonly MongoDbContainer _mongoDbContainer = new MongoDbBuilder().Build();

	private IDatabaseSettings? DbConfig { get; set; }

	public PlaywrightFixture(IMessageSink output) : base(output)
	{
	}

	protected override IHost CreateHost(IHostBuilder builder)
	{
		builder.AddTestConfiguration();
		builder.UseOnlyTestContainer();
		builder.UseUniqueDb(_uniqueId);
		var host = base.CreateHost(builder);

		return host;
	}

	// Temp hack to see if it is a timing issue in github actions
	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();

		await _mongoDbContainer.StartAsync();
		string connString = _mongoDbContainer.GetConnectionString();
		string dbName = $"test_{Guid.NewGuid():N}";
		DbConfig = new DatabaseSettings(connString, databaseName: dbName) { ConnectionStrings = connString, DatabaseName = dbName };


		Services.AddSingleton<IDatabaseSettings>(DbConfig);

		Services.AddSingleton<IMongoDbContextFactory>(new MongoDbContextFactory(DbConfig));

		await Services.ApplyStartUpDelay();
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize",
		Justification = "Base class calls SuppressFinalize")]
	public override async ValueTask DisposeAsync()
	{
		await base.DisposeAsync();

		var logger = MessageSink.CreateLogger<PlaywrightFixture>();
		await _uniqueId.CleanUpDbFilesAsync(logger);
	}
}
