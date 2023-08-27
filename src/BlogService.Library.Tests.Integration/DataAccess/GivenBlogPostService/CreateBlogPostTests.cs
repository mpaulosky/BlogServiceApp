// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : CreateBlogPostTests.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.DataAccess.GivenBlogPostService;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class CreateBlogPostTests : IAsyncLifetime
{
	private const string CleanupValue = "posts";

	private readonly IntegrationTestFactory _factory;
	private readonly BlogPostService _sut;

	public CreateBlogPostTests(IntegrationTestFactory factory)
	{
		_factory = factory;
		IBlogPostData postData = _factory.Services.GetRequiredService<IBlogPostData>();
		_sut = new BlogPostService(postData);
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
	public async Task CreateAsync_With_ValidData_Should_CreateABlogPost_TestAsync()
	{
		// Arrange
		BlogPost expected = BlogPostCreator.GetNewBlogPost();

		// Act
		await _sut.CreateAsync(expected);

		// Assert
		expected.Id.Should().NotBeNull();
	}

	[Fact]
	public async Task CreateAsync_With_InValidData_Should_FailToCreateABlogPost_TestAsync()
	{
		// Arrange

		// Act

		// Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.CreateAsync(null!));
	}
}
