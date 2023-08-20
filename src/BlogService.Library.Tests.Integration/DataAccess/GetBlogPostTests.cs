// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GetBlogPostTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : IssueTracker
// Project Name :  BlBlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.DataAccess;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class GetBlogPostTests : IAsyncLifetime
{
	private const string CleanupValue = "posts";

	private readonly IntegrationTestFactory _factory;
	private readonly BlogPostService _sut;

	public GetBlogPostTests(IntegrationTestFactory factory)
	{
		_factory = factory;
		IBlogPostData postData = _factory.Services.GetRequiredService<IBlogPostData>();
		_sut = new BlogPostService(postData);	}

	public Task InitializeAsync()
	{
		return Task.CompletedTask;
	}

	public async Task DisposeAsync()
	{
		await _factory.ResetCollectionAsync(CleanupValue);
	}

	[Fact]
	public async Task GetAsync_With_WithData_Should_ReturnAValidBlogPost_TestAsync()
	{
		// Arrange
		BlogPost expected = BlogPostCreator.GetNewBlogPost(true);
		await _sut.CreateAsync(expected);

		// Act
		BlogPost result = await _sut.GetByUrlAsync(expected.Url);

		// Assert
		result.Should().BeEquivalentTo(expected, options => options
			.Excluding(x => x.Created)
			.Excluding(t => t.Updated));
	}

	[Theory]
	[InlineData("62cf2ad6326e99d665759e5a")]
	public async Task GetAsync_With_WithoutData_Should_ReturnNothing_TestAsync(string value)
	{
		// Arrange

		// Act
		BlogPost result = await _sut.GetByUrlAsync(value);

		// Assert
		result.Should().BeNull();
	}
}
