// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     WhenRequireingABlogPostService.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library.Tests.Unit
// =============================================

namespace BlogService.Library.Tests.Unit.Services.GivenABlogPostService;

[ExcludeFromCodeCoverage]
public class WhenRequiringABlogPostService
{
  private readonly Mock<IBlogPostData> _mockData = new();

  private BlogPostService SystemUnderTest()
  {
	return new BlogPostService(_mockData.Object);
  }

  [Fact]
  public async Task GetAllAsync_ReturnsAllBlogPosts()
  {
	// Arrange
	var expected = BlogPostCreator.GetBlogPosts(3).ToList();

	_mockData.Setup(d => d.GetAllAsync()).ReturnsAsync(expected);

	var sut = SystemUnderTest();

	// Act
	var actual = await sut.GetAllAsync();

	// Assert
	actual.Should().BeEquivalentTo(expected);

	_mockData.Verify(d => d.GetAllAsync(), Times.Once);
  }

  [Fact]
  public async Task GetByUrlAsync_ReturnsBlogPostWithMatchingUrl()
  {
	// Arrange
	var expected = BlogPostCreator.GetNewBlogPost(true);

	_mockData.Setup(d => d.GetByUrlAsync(expected.Url)).ReturnsAsync(expected);

	var sut = SystemUnderTest();

	// Act
	var actual = await sut.GetByUrlAsync(expected.Url);

	// Assert
	actual.Should().BeEquivalentTo(expected);

	_mockData.Verify(d => d.GetByUrlAsync(expected.Url), Times.Once);
  }

  [Fact]
  public async Task ArchiveAsync_DoesNotThrowException()
  {
	// Arrange
	var postToArchive = BlogPostCreator.GetNewBlogPost(true);
	postToArchive.IsDeleted = true;

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
	var postToUpdate = BlogPostCreator.GetNewBlogPost(true);

	_mockData.Setup(d => d.UpdateAsync(postToUpdate)).Returns(Task.CompletedTask);

	var sut = SystemUnderTest();

	// Act
	await sut.UpdateAsync(postToUpdate);

	// Assert
	_mockData.Verify(d => d.UpdateAsync(postToUpdate), Times.Once);
  }

  [Fact]
  public async Task CreateAsync_ReturnsBlogPostInstance()
  {
	// Arrange
	var expected = BlogPostCreator.GetNewBlogPost();

	_mockData.Setup(d => d.CreateAsync(expected));

	var sut = SystemUnderTest();

	// Act
	await sut.CreateAsync(expected);

	// Assert
	_mockData.Verify(d => d.CreateAsync(expected), Times.Once);
  }
}
