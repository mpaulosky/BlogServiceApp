// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GetBlogPostTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : IssueTracker
// Project Name :  BloBlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.DataAccess;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class GetBlogPostsTests : IAsyncLifetime
{
	private const string CleanupValue = "posts";

	private readonly IntegrationTestFactory _factory;
	private readonly BlogPostService _sut;

	public GetBlogPostsTests(IntegrationTestFactory factory)
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
	public async Task GetAllAsync_With_ValidData_Should_ReturnBlogPost_Test()
	{
		// Arrange
		BlogPost expected = BlogPostCreator.GetNewBlogPost(true);
		await _sut.CreateAsync(expected);

		// Act
		List<BlogPost> results = (await _sut.GetAllAsync()).ToList();

		// Assert
		results.Count.Should().Be(1);
		results.First().Should().BeEquivalentTo(expected, options => options
			.Excluding(x => x.Created)
			.Excluding(t=> t.Updated));
	}
}
