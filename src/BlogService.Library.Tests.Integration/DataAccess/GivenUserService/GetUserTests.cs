// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : GetUserTests.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.DataAccess.GivenUserService;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class GetUserTests : IAsyncLifetime
{
	private const string CleanupValue = "users";

	private readonly IntegrationTestFactory _factory;
	private readonly UserService _sut;

	public GetUserTests(IntegrationTestFactory factory)
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
	public async Task GetAsync_With_WithData_Should_ReturnAValidUser_TestAsync()
	{
		// Arrange
		User expected = UserCreator.GetNewUser();
		await _sut.CreateAsync(expected);

		// Act
		User result = await _sut.GetAsync(expected.Id);

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	[Theory]
	[InlineData("62cf2ad6326e99d665759e5a")]
	public async Task GetAsync_With_WithoutData_Should_ReturnNothing_TestAsync(string value)
	{
		// Arrange

		// Act
		User result = await _sut.GetAsync(value!);

		// Assert
		result.Should().BeNull();
	}
}
