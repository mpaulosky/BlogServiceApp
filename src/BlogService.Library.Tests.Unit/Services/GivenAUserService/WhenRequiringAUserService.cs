// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : WhenRequiringAUserService.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Unit
// =============================================

namespace BlogService.Library.Tests.Unit.Services.GivenAUserService;

[ExcludeFromCodeCoverage]
public class WhenRequiringAUserService
{
	private readonly Mock<IUserData> _mockData = new();

	private UserService SystemUnderTest()
	{
		return new UserService(_mockData.Object);
	}

	[Fact]
	public async Task GetAllAsync_ReturnsAllUsers()
	{
		// Arrange
		var expected = UserCreator.GetUsers(3).ToList();

		_mockData.Setup(d => d.GetAllAsync()).ReturnsAsync(expected);

		var sut = SystemUnderTest();

		// Act
		var actual = await sut.GetAllAsync();

		// Assert
		actual.Should().BeEquivalentTo(expected);

		_mockData.Verify(d => d.GetAllAsync(), Times.Once);
	}

	[Fact]
	public async Task ArchiveAsync_DoesNotThrowException()
	{
		// Arrange
		var postToArchive = UserCreator.GetNewUser(true);
		postToArchive.Archived = true;
		postToArchive.ArchivedBy = BasicUserCreator.GetNewBasicUser(true);

		_mockData.Setup(d => d.ArchiveAsync(postToArchive)).Returns(Task.CompletedTask);

		var sut = SystemUnderTest();

		// Act
		await sut.ArchiveAsync(postToArchive);

		// Assert
		_mockData.Verify(d => d.ArchiveAsync(postToArchive), Times.Once);
	}

	[Fact]
	public async Task UpdateAsync_DoesNotThrowException()
	{
		// Arrange
		var postToUpdate = UserCreator.GetNewUser(true);

		_mockData.Setup(d => d.UpdateAsync(postToUpdate)).Returns(Task.CompletedTask);

		var sut = SystemUnderTest();

		// Act
		await sut.UpdateAsync(postToUpdate);

		// Assert
		_mockData.Verify(d => d.UpdateAsync(postToUpdate), Times.Once);
	}

	[Fact]
	public async Task CreateAsync_ReturnsUserInstance()
	{
		// Arrange
		var expected = UserCreator.GetNewUser();

		_mockData.Setup(d => d.CreateAsync(expected));

		var sut = SystemUnderTest();

		// Act
		await sut.CreateAsync(expected);

		// Assert
		_mockData.Verify(d => d.CreateAsync(expected), Times.Once);
	}
}
