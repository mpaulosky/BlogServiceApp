// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : UpdateBlogPostTests.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Integration
// =============================================

namespace BlogService.Library.DataAccess.GivenBlogPostService;

[ExcludeFromCodeCoverage]
[Collection("Test Collection")]
public class UpdateBlogPostTests : IAsyncLifetime
{
	private const string CleanupValue = "posts";

	private readonly IntegrationTestFactory _factory;
	private readonly BlogPostService _sut;

	public UpdateBlogPostTests(IntegrationTestFactory factory)
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
	public async Task UpdateAsync_With_ValidData_Should_UpdateTheBlogPost_Test()
	{
		// Arrange
		BlogPost expected = BlogPostCreator.GetNewBlogPost(true);
		await _sut.CreateAsync(expected);

		// Act
		expected.Title = "Updated";
		await _sut.UpdateAsync(expected);
		BlogPost result = await _sut.GetByUrlAsync(expected.Url);

		// Assert
		result.Should().BeEquivalentTo(expected, options => options
			.Excluding(x => x.Created)
			.Excluding(t => t.Updated));
	}

	[Fact]
	public async Task UpdateAsync_With_WithInValidData_Should_ThrowArgumentNullException_Test()
	{
		// Arrange

		// Act
		Func<Task> act = async () => await _sut.UpdateAsync(null);

		// Assert
		await act.Should().ThrowAsync<NullReferenceException>();
	}
}
