// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     MongoDbContextFactoryTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.DataAccess;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class MongoDbContextFactoryTests : IAsyncLifetime
{
	private const string CleanupValue = "";

	private readonly IntegrationTestFactory _factory;
	private readonly IMongoDbContextFactory _dbContext;

	public MongoDbContextFactoryTests(IntegrationTestFactory factory)
	{
		_factory = factory;
		_dbContext = _factory.Services.GetRequiredService<IMongoDbContextFactory>();
	}

	public Task InitializeAsync()
	{
		return Task.CompletedTask;
	}

	public async Task DisposeAsync()
	{
		await _factory.ResetCollectionAsync(CleanupValue);
	}

	[Fact]
	public void GetCollection_With_Valid_DbContext_Should_Return_Value_Test()
	{
		// Arrange
		const string name = "users";

		// Act
		IMongoCollection<User> result = _dbContext.GetCollection<User>(name);

		// Assert
		result.Should().NotBeNull();
		_dbContext.DbName.Should().NotBeNull();
		_dbContext.ConnectionString.Should().NotBeNull();
	}

	[Fact]
	public void ConnectionStateReturnsOpen()
	{
		// Given
		IMongoClient client = _dbContext.Client;

		// When
		using IAsyncCursor<BsonDocument> databases = client.ListDatabases();

		// Then
		Assert.Contains(databases.ToEnumerable(),
			database => database.TryGetValue("name", out BsonValue name) && "admin".Equals(name.AsString));
	}

	[Fact]
	public async Task Be_healthy_if_mongodb_is_available()
	{
		// Arrange
		TestServer sut = _factory.Server;

		// Act
		HttpResponseMessage response = await sut.CreateRequest("/health").GetAsync();

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}
