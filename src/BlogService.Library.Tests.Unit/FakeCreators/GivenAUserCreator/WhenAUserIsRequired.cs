// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     WhenAUserIsRequired.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library.Tests.Unit
// =============================================

namespace BlogService.Library.Tests.Unit.FakeCreators.GivenAUserCreator;

[ExcludeFromCodeCoverage]
public class WhenAUserIsRequired
{
  [Fact]
  public void ShouldReturnNewUserWithoutId_Test()
  {
	// Arrange
	var expected = UserCreator.GetNewUser()!;

	// Act
	var result = UserCreator.GetNewUser();

	// Assert
	result.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void ShouldReturnNewUserWithId_Test()
  {
	// Arrange
	var expected = UserCreator.GetNewUser(true)!;

	// Act
	var result = UserCreator.GetNewUser(true);

	// Assert

	result.Should().BeEquivalentTo(expected, options => options
		.Excluding(t => t.Id)
		.Excluding(t => t.ObjectIdentifier));
  }

  [Fact]
  public void ShouldReturnDifferentNewUserWhenNewSeedIsTrue_Test()
  {
	// Arrange
	var expected = UserCreator.GetNewUser(true)!;

	// Act
	var result = UserCreator.GetNewUser(true, true);

	// Assert
	result.Should().NotBeEquivalentTo(expected);
  }

  [Fact]
  public void ShouldReturnAListOfNewUsers_Test()
  {
	// Arrange
	var expected = UserCreator.GetUsers(3)!;

	// Act
	IEnumerable<User> result = UserCreator.GetUsers(3);

	// Assert
	result.Should().BeEquivalentTo(expected, options => options
		.Excluding(t => t.Id)
		.Excluding(t => t.ObjectIdentifier));
  }

  [Fact]
  public void ShouldReturnAListOfUsers_Test()
  {
	// Arrange
	var expected = UserCreator.GetUsers(3)!;

	// Act
	IEnumerable<User> result = UserCreator.GetUsers(3).ToList();

	// Assert
	result.Should().BeEquivalentTo(expected, options => options
		.Excluding(t => t.Id)
		.Excluding(t => t.ObjectIdentifier));
  }
}
