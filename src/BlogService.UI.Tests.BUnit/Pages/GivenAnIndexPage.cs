// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GivenAnIndexPage.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.BUnit
// =============================================

namespace BlogService.UI.Tests.BUnit.Pages;

[ExcludeFromCodeCoverage]
public class GivenAnIndexPage : TestContext
{
	private readonly Mock<IBlogPostData> _blogPostDataMock = new();
	private List<BlogPost>? _expectedPosts = new();

	private IRenderedComponent<Index> ComponentUnderTest()
	{
		IRenderedComponent<Index> component = RenderComponent<Index>();

		return component;
	}

	[Fact()]
	public void Index_With_ValidData_Should_DisplayData_Test()
	{
		// Arrange
		const string expected =
			"""
			<h1>Welcome to my blog!</h1>
			Here are my latest posts, enjoy!
			<div class="card my-4">
			<div class="card-img" style="background-image: url('https://picsum.photos/1060/300/?image=12');"></div>
			<div class="card-body">
			<h5 diff:ignore></h5>
			<p diff:ignore></p>
			<p class="card-text">
			 <small diff:ignore></small>
			</p>
			<a href:ignore class="btn btn-primary">Read more...</a>
			</div>
			</div>
			<div class="card my-4">
			<div class="card-img" style="background-image: url('https://picsum.photos/1060/300/?image=12');"></div>
			<div class="card-body">
			<h5 diff:ignore></h5>
			<p diff:ignore></p>
			<p class="card-text">
			<small diff:ignore></small>
			</p>
			<a href:ignore class="btn btn-primary">Read more...</a>
			</div>
			</div>
			<div class="card my-4">
			<div class="card-img" style="background-image: url('https://picsum.photos/1060/300/?image=12');"></div>
			<div class="card-body">
			<h5 diff:ignore></h5>
			<p diff:ignore></p>
			<p class="card-text">
			<small diff:ignore></small>
			</p>
			<a href:ignore class="btn btn-primary">Read more...</a>
			</div>
			</div>
			""";

		_expectedPosts = BlogPostCreator.GetBlogPosts(3).ToList();

		SetupMocks();
		RegisterServices();

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
	}

	[Fact()]
	public void Index_With_NoData_Should_DisplayNoData_Test()
	{
		// Arrange
		const string expected =
			"""
			<h1>Welcome to my blog!</h1>
			Here are my latest posts, enjoy!
			""";

		SetupMocks();
		RegisterServices();

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
	}

	private void SetupMocks()
	{
		_blogPostDataMock.Setup(x => x
				.GetAllAsync())
			.ReturnsAsync(_expectedPosts);
	}

	private void RegisterServices()
	{
		Services.AddSingleton<IBlogService>(new BlogPostService(_blogPostDataMock.Object));
	}
}
