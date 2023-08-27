// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : WhenTestBlogPostDtoRequired.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Unit
// =============================================

namespace BlogService.Library.Tests.Unit.FakeCreators.GivenBlogPostDtoCreator;

[ExcludeFromCodeCoverage]
public class WhenTestBlogPostDtoRequired
{
	[Fact]
	public void ShouldReturnNewBlogPostDtoWithSameSeed_Test()
	{
		// Arrange
		var expected = BlogPostDtoCreator.GetNewBlogPostDto()!;

		// Act
		var result = BlogPostDtoCreator.GetNewBlogPostDto();

		// Assert
		result.Should().BeEquivalentTo(expected,
			options => options
				.Excluding(t => t.Created));
	}

	[Fact]
	public void ShouldReturnNewBlogPostDtoDifferentSeed_Test()
	{
		// Arrange
		var expected = BlogPostDtoCreator.GetNewBlogPostDto(true)!;

		// Act
		var result = BlogPostDtoCreator.GetNewBlogPostDto(true);

		// Assert

		result.Should().NotBeEquivalentTo(expected);
	}
}
