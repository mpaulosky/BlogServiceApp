// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : IntegrationTestFactory.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.Fixtures;

[Collection("Test collection")]
[ExcludeFromCodeCoverage]
[UsedImplicitly]
public class IntegrationTestFactory : WebApplicationFactory<IAppMarker>, IAsyncLifetime
{
	private readonly string _databaseName = $"blazorservice_{Guid.NewGuid():N}";

	private readonly MongoDbContainer _mongoDbContainer = new MongoDbBuilder().Build();

	private IDatabaseSettings DbSettings { get; set; }
	private string ConnectionString { get; set; }
	private IMongoDbContextFactory DbContext { get; set; }

	public async Task InitializeAsync()
	{
		await _mongoDbContainer.StartAsync();
		ConnectionString = _mongoDbContainer.GetConnectionString();
		DbSettings = new DatabaseSettings(ConnectionString, _databaseName)
		{
			ConnectionStrings = ConnectionString, DatabaseName = _databaseName
		};
	}

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseEnvironment("Development");

		builder.UseUniqueDb(DbSettings);

		builder.ConfigureTestServices(services =>
		{
			services.CleanupServices();

			services.ReplaceRemovedServices(DbSettings);

			using ServiceProvider serviceProvider = services.BuildServiceProvider();

			DbContext = serviceProvider.GetRequiredService<IMongoDbContextFactory>();
		});
	}

	public new async Task DisposeAsync()
	{
		await DbContext.Client.DropDatabaseAsync(_databaseName);
		await _mongoDbContainer.DisposeAsync().ConfigureAwait(false);
	}

	public async Task ResetCollectionAsync(string collection)
	{
		if (!string.IsNullOrWhiteSpace(collection))
		{
			await DbContext.Client.GetDatabase(_databaseName).DropCollectionAsync(collection);
		}
	}
}
