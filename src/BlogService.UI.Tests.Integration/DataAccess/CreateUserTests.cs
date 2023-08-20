// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     CreateUserTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : IssueTracker
// Project Name :  BlogService.UI.Tests.Integration
// =============================================

namespace IssueTracker.PlugIns.DataAccess;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class CreateUserTests : IAsyncLifetime
{
	private const string CleanupValue = "users";

	private readonly IntegrationTestFactory _factory;
	private readonly UserService _sut;

	public CreateUserTests(IntegrationTestFactory factory)
	{
		_factory = factory;
		IUserData userData = _factory.Services.GetRequiredService<IUserData>();
		_sut = new UserService(userData);	}

	public Task InitializeAsync()
	{
		return Task.CompletedTask;
	}

	public async Task DisposeAsync()
	{
		await _factory.ResetCollectionAsync(CleanupValue);
	}

	[Fact]
	public async Task CreateAsync_With_ValidData_Should_CreateAUser_TestAsync()
	{
		// Arrange
		User expected = UserCreator.GetNewUser();

		// Act
		await _sut.CreateAsync(expected);

		// Assert
		expected.Id.Should().NotBeNull();
	}

	[Fact]
	public async Task CreateAsync_With_InValidData_Should_FailToCreateAUser_TestAsync()
	{
		// Arrange

		// Act

		// Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.CreateAsync(null!));
	}
}
