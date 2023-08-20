/*
// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     MongoBlogPostDataTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library.Tests.Unit
// =============================================

namespace BlogService.Library.Tests.Unit.DataAccess;

public class MongoBlogPostDataTests
{
	private readonly Mock<IMongoCollection<BlogPost>> _mockCollection;
	private readonly Mock<IMongoDbContextFactory> _mockContext;
	private List<BlogPost> _list = new();

	public MongoBlogPostDataTests()
	{
		var cursor = TestFixtures.GetMockCursor(_list);

		_mockCollection = TestFixtures.GetMockCollection(cursor);

		_mockContext = TestFixtures.GetMockContext();
	}

	private MongoBlogPostData SystemUnderTest()
	{
		return new MongoBlogPostData(_mockContext.Object);
	}

	[Fact]
	public async Task ArchiveAsync_ShouldArchivePost()
	{
		//Arrange
		var sut = SystemUnderTest();

		var post = new BlogPost { Id = "1", Content = "content" };

		//Act
		await sut.ArchiveAsync(post);

		//Assert
		var result = await sut.GetAsync("1");
		result.Should().NotBeNull();
		result.Content.Should().BeEquivalentTo(post.Content);
	}

	[Fact]
	public async Task CreateAsync_ShouldCreateNewPost()
	{
		//Arrange
		var sut = SystemUnderTest();

		var post = new BlogPost { Id = "1", Content = "content" };

		//Act
		await sut.CreateAsync(post);

		//Assert
		var result = await sut.GetAsync("1");
		result.Should().NotBeNull();
		result.Content.Should().BeEquivalentTo(post.Content);
	}

	[Fact]
	public async Task GetAllAsync_ShouldGetAllPosts()
	{
		//Arrange
		var sut = SystemUnderTest();

		var posts = new List<BlogPost>
		{
			new BlogPost { Id = "1", Content = "content1" },
			new BlogPost { Id = "2", Content = "content2" },
			new BlogPost { Id = "3", Content = "content3" }
		};

		foreach (var post in posts)
		{
			await sut.CreateAsync(post);
		}

		//Act
		var result = await sut.GetAllAsync();

		//Assert
		result.Should().NotBeNull();
		result.Should().BeEquivalentTo(posts);
	}

	[Fact]
	public async Task GetAsync_ShouldGetPostById()
	{
		//Arrange
		var sut = SystemUnderTest();

		var post = new BlogPost { Id = "1", Content = "content" };
		await sut.CreateAsync(post);

		//Act
		var result = await sut.GetAsync("1");

		//Assert
		result.Should().NotBeNull();
		result.Id.Should().BeEquivalentTo(post.Id);
		result.Content.Should().BeEquivalentTo(post.Content);
	}

	[Fact]
	public async Task GetByUrlAsync_ShouldGetPostByUrl()
	{
		//Arrange
		var sut = SystemUnderTest();

		var post = new BlogPost { Id = "1", Content = "content", Url = "testurl" };
		await sut.CreateAsync(post);

		//Act
		var result = await sut.GetByUrlAsync("testurl");

		//Assert
		result.Should().NotBeNull();
		result.Id.Should().BeEquivalentTo(post.Id);
		result.Content.Should().BeEquivalentTo(post.Content);
		result.Url.Should().BeEquivalentTo(post.Url);
	}

	[Fact]
	public async Task UpdateAsync_ShouldUpdatePost()
	{
		//Arrange
		var sut = SystemUnderTest();

		var post = new BlogPost { Id = "1", Content = "content" };
		await sut.CreateAsync(post);

		post.Content = "updated content";

		//Act
		await sut.UpdateAsync(post);

		//Assert
		var result = await sut.GetAsync("1");
		result.Should().NotBeNull();
		result.Content.Should().BeEquivalentTo(post.Content);
	}
}
*/
