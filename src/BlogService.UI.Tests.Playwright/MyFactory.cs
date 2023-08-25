// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     MyFactory.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI.Tests.Playwright;

internal class MyFactory : WebApplicationFactory<IAppMarker>
{
	private readonly string _databaseName;
	private readonly MongoDbContainer _mongoDbContainer;
	public IMongoDbContextFactory DbContext { get; set; }
	private IDatabaseSettings DbConfig { get; set; }

	public MyFactory()
	{
		_databaseName = $"test_{Guid.NewGuid():N}";

		try
		{
			_mongoDbContainer = new MongoDbBuilder().Build();
		}
		catch (ArgumentException ae)
		{
			throw new XunitException($"Is docker installed and running? {ae.Message}.");
		}
	}

	protected override IHost CreateHost(IHostBuilder builder)
	{
		// need to create a plain host that we can return.
		var dummyHost = builder.Build();

		// configure and start the actual host.
		builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

		var host = builder.Build();
		host.Start();

		return dummyHost;
	}

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseUrls("https://localhost:7048");
		builder.UseEnvironment("FullIntegrationTest").ConfigureTestServices(services =>
		{
			Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "FullIntegrationTest");

			ServiceDescriptor? dbConnectionDescriptor =
				services.SingleOrDefault(d => d.ServiceType == typeof(IMongoDbContextFactory));

			services.Remove(dbConnectionDescriptor!);

			ServiceDescriptor? dbSettings =
				services.SingleOrDefault(d => d.ServiceType == typeof(IDatabaseSettings));

			services.Remove(dbSettings!);

			DbConfig = new DatabaseSettings(_mongoDbContainer.GetConnectionString(), databaseName: _databaseName) { ConnectionStrings = connString, DatabaseName = dbName };

			services.AddSingleton(DbConfig);

			services.AddSingleton<IMongoDbContextFactory>(_ => new MongoDbContextFactory(DbConfig));

			using ServiceProvider serviceProvider = services.BuildServiceProvider();

			DbContext = serviceProvider.GetRequiredService<IMongoDbContextFactory>();
		});
	}
}
