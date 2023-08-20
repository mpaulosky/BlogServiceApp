// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     ArchiveBlogPostTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.DataAccess;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class ArchiveBlogPostTests : IAsyncLifetime
{
	private const string CleanupValue = "posts";

	private readonly IntegrationTestFactory _factory;
	private readonly BlogPostService _sut;

	public ArchiveBlogPostTests(IntegrationTestFactory factory)
	{
		_factory = factory;
		_factory.Services.GetRequiredService<IMongoDbContextFactory>();
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

	[Fact(DisplayName = "Archive BlogPost With Valid Data (Archive)")]
	public async Task ArchiveAsync_With_ValidData_Should_ArchiveABlogPost_TestAsync()
	{
		// Arrange
		BlogPost expected = BlogPostCreator.GetNewBlogPost();
		expected.IsDeleted = true;

		await _sut.CreateAsync(expected);

		// Act
		await _sut.ArchiveAsync(expected);

		BlogPost result = await _sut.GetByUrlAsync(expected.Url);

		// Assert
		result.Should().NotBeNull();
		result.Id.Should().Be(expected.Id);
		result.IsDeleted.Should().BeTrue();
	}
}
