// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     ArchiveUserTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : IssueTracker
// Project Name :  BlBlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.DataAccess;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class ArchiveUserTests : IAsyncLifetime
{
	private const string CleanupValue = "users";

	private readonly IntegrationTestFactory _factory;
	private readonly UserService _sut;

	public ArchiveUserTests(IntegrationTestFactory factory)
	{
		_factory = factory;
		_factory.Services.GetRequiredService<IMongoDbContextFactory>();
		IUserData userData = _factory.Services.GetRequiredService<IUserData>();
		_sut = new UserService(userData);
	}

	[Fact]
	public Task InitializeAsync()
	{
		return Task.CompletedTask;
	}

	[Fact]
	public async Task DisposeAsync()
	{
		await _factory.ResetCollectionAsync(CleanupValue);
	}

	[Fact(DisplayName = "Archive User With Valid Data (Archive)")]
	public async Task ArchiveAsync_With_ValidData_Should_ArchiveAUser_TestAsync()
	{
		// Arrange
		User expected = UserCreator.GetNewUser();
		expected.Archived = true;
		expected.ArchivedBy = BasicUserCreator.GetNewBasicUser(true);

		await _sut.CreateAsync(expected);

		// Act
		await _sut.ArchiveAsync(expected);

		User result = await _sut.GetAsync(expected.Id);

		// Assert
		result.Should().NotBeNull();
		result.Id.Should().Be(expected.Id);
		result.Archived.Should().BeTrue();
		result.ArchivedBy.Should().BeEquivalentTo(expected.ArchivedBy);
	}
}
