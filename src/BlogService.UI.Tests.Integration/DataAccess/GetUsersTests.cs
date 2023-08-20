// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GetUsersTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : IssueTracker
// Project Name :  BlogService.UI.Tests.Integration
// =============================================

namespace IssueTracker.PlugIns.DataAccess;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class GetUsersTests : IAsyncLifetime
{
	private const string CleanupValue = "users";

	private readonly IntegrationTestFactory _factory;
	private readonly UserService _sut;

	public GetUsersTests(IntegrationTestFactory factory)
	{
		_factory = factory;
		IUserData userData = _factory.Services.GetRequiredService<IUserData>();
		_sut = new UserService(userData);
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
	public async Task GetAllAsync_With_ValidData_Should_ReturnUsers_Test()
	{
		// Arrange
		User expected = UserCreator.GetNewUser();
		await _sut.CreateAsync(expected);

		// Act
		List<User> results = (await _sut.GetAllAsync()).ToList();

		// Assert
		results.Count.Should().Be(1);
		results.First().DisplayName.Should().Be(expected.DisplayName);
		results.First().FirstName.Should().Be(expected.FirstName);
		results.First().LastName.Should().Be(expected.LastName);
	}
}
