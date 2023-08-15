// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     WhenRequiringABlogPost.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library.Tests.Unit
// =============================================

namespace BlogService.Library.Tests.Unit.GivenBlogPostDto;

[ExcludeFromCodeCoverage]
public class WhenRequiringABlogPost
{
	[Fact]
	public void ShouldConvertBlogPostDtoToBlogPost_Test()
	{
		// Arrange
		var expectedDto = BlogPostDtoCreator.GetNewBlogPostDto()!;

		// Act
		var result = expectedDto.ToBlogPost();

		// Assert
		result.Should().BeEquivalentTo(expectedDto,
			options => options
				.Excluding(t => t.Created)
				.Excluding(t => t.IsPublished));
	}

	[Fact]
	public void ShouldConvertBlogPostDtoToBlogPostWithNewSeed_Test()
	{
		// Arrange
		var expectedDto = BlogPostDtoCreator.GetNewBlogPostDto(true)!;

		// Act
		var result = expectedDto.ToBlogPost();

		// Assert
		result.Should().BeEquivalentTo(expectedDto,
			options => options
				.Excluding(t => t.Created)
				.Excluding(t => t.IsPublished));
	}
}
