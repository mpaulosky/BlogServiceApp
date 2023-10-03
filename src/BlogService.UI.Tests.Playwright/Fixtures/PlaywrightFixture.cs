// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     PlaywrightFixture.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.Playwright
// =============================================

using TestingSupport.Library.Fixtures;

namespace BlogService.UI.Fixtures;

/// <summary>
///   WebApplicationFactory that wraps the TestHost in a Kestrel server and provides Playwright and HttpClient testing.
///   This also logs output from the Host under test to XUnit.
///   <p>
///     Credit to <a href="https://github.com/CZEMacLeod">https://github.com/CZEMacLeod</a> for writing this.
///     Functionality is now wrapped in the nuget package C3D.Extensions.Playwright.AspNetCore.XUnit
///   </p>
/// </summary>
[ExcludeFromCodeCoverage]
[UsedImplicitly]
public class PlaywrightFixture : PlaywrightFixture<AssemblyClassLocator>
{
	public PlaywrightFixture(IMessageSink output) : base(output)
	{
	}

	public override string Environment { get; } = "Development";

	private readonly string _databaseName = $"blazorservice_{Guid.NewGuid():N}";
	private string MongoConnectionString { get; set; } = string.Empty;

	private readonly MongoDbContainer _mongoDbContainer = new MongoDbBuilder().Build();

	private IDatabaseSettings DatabaseSettings { get; set; } = null!;

	public IMongoDbContextFactory DbContext { get; set; } = null!;

	// Temp hack to see if it is a timing issue in github actions
	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();

		await _mongoDbContainer.StartAsync();

		MongoConnectionString = _mongoDbContainer.GetConnectionString();

		DatabaseSettings = new DatabaseSettings(connectionStrings: MongoConnectionString, databaseName: _databaseName)
		{
			ConnectionStrings = MongoConnectionString, DatabaseName = _databaseName
		};

		DbContext = Services.GetDatabaseContext();

		await Services.ApplyStartUpDelay();
	}

	protected override IHost CreateHost(IHostBuilder builder)
	{
		builder.AddTestConfiguration();
		builder.AddTestInMemoryMongoDbSection(DatabaseSettings);
		builder.UpdateDatabaseServices(DatabaseSettings);

		var host = base.CreateHost(builder);

		return host;
	}

	[SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize",
		Justification = "Base class calls SuppressFinalize")]
	public override async ValueTask DisposeAsync()
	{
		await base.DisposeAsync();
		await DbContext.Client.DropDatabaseAsync(_databaseName);
		await _mongoDbContainer.DisposeAsync().ConfigureAwait(false);
	}
}
