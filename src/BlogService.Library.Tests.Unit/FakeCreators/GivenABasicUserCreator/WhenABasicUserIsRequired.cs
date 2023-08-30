// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     WhenABasicUserIsRequired.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library.Tests.Unit
// =============================================

namespace BlogService.Library.Tests.Unit.FakeCreators.GivenABasicUserCreator;

[ExcludeFromCodeCoverage]
public class WhenABasicUserIsRequired
{
	[Fact]
	public void ShouldReturnNewBasicUserWithoutId_Test()
	{
		// Arrange
		var expected = BasicUserCreator.GetNewBasicUser();

		// Act
		var result = BasicUserCreator.GetNewBasicUser();

		// Assert
		result.Should().BeEquivalentTo(expected,
			options => options
				.Excluding(t => t.Id));
	}

	[Fact]
	public void ShouldReturnNewBasicUserWithId_Test()
	{
		// Arrange
		var expected = BasicUserCreator.GetNewBasicUser(true)!;

		// Act
		var result = BasicUserCreator.GetNewBasicUser(true);

		// Assert

		result.Should().BeEquivalentTo(expected,
			options => options
				.Excluding(t => t.Id));
	}

	[Fact]
	public void ShouldReturnDifferentNewBasicUserWhenNewSeedIsTrue_Test()
	{
		// Arrange
		var expected = BasicUserCreator.GetNewBasicUser(true)!;

		// Act
		var result = BasicUserCreator.GetNewBasicUser(true, true);

		// Assert
		result.Should().NotBeEquivalentTo(expected);
	}

	[Fact]
	public void ShouldReturnAListOfNewBasicUsers_Test()
	{
		// Arrange
		var expected = BasicUserCreator.GetBasicUsers(3)!;

		// Act
		IEnumerable<BasicUser> result = BasicUserCreator.GetBasicUsers(3);

		// Assert
		result.Should().BeEquivalentTo(expected,
			options => options
				.Excluding(t => t.Id));
	}

	[Fact]
	public void ShouldReturnAListOfBasicUsers_Test()
	{
		// Arrange
		var expected = BasicUserCreator.GetBasicUsers(3)!;

		// Act
		IEnumerable<BasicUser> result = BasicUserCreator.GetBasicUsers(3).ToList();

		// Assert
		result.Should().BeEquivalentTo(expected,
			options => options
				.Excluding(t => t.Id));
	}
}
