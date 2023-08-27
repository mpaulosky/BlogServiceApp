// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : UpdateUserTests.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.DataAccess.GivenUserService;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class UpdateUserTests : IAsyncLifetime
{
	private const string CleanupValue = "users";

	private readonly IntegrationTestFactory _factory;
	private readonly UserService _sut;

	public UpdateUserTests(IntegrationTestFactory factory)
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
	public async Task UpdateAsync_With_ValidData_Should_UpdateTheUser_Test()
	{
		// Arrange
		User expected = UserCreator.GetNewUser();
		await _sut.CreateAsync(expected);

		// Act
		expected.DisplayName = "Updated";
		await _sut.UpdateAsync(expected);
		User result = await _sut.GetAsync(expected.Id);

		// Assert
		result.Should().BeEquivalentTo(expected);
	}

	[Fact]
	public async Task UpdateAsync_With_WithInValidData_Should_ThrowArgumentNullException_Test()
	{
		// Arrange

		// Act
		Func<Task> act = async () => await _sut.UpdateAsync(null!);

		// Assert
		await act.Should().ThrowAsync<NullReferenceException>();
	}
}
