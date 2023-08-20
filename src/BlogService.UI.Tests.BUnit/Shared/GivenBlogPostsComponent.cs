// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GivenBlogPostsComponent.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.BUnit
// =============================================

namespace BlogService.UI.Tests.BUnit.Shared;

[ExcludeFromCodeCoverage]
public class GivenBlogPostsComponent : TestContext
{
	private readonly Mock<IBlogService> _blogServiceMock = new();
	private List<BlogPost>? _expectedPosts = new();

	private IRenderedComponent<BlogPosts> ComponentUnderTest()
	{
		IRenderedComponent<BlogPosts> component = RenderComponent<BlogPosts>();

		return component;
	}

	[Fact]
	public void BlogPosts_With_Data_Should_DisplayPosts_Test()
	{
		// Arrange
		const string expected =
			"""
			<div class="card my-4">
			<div class="card-img" style="background-image: url('https://picsum.photos/1060/300/?image=12');"></div>
			<div class="card-body">
			<h5 diff:ignore class="card-title">Iste quia natus et dignissimos reiciendis ad nostrum totam harum.</h5>
			<p diff:ignore class="card-text">Doloremque eos asperiores cum ipsam illum pariatur doloribus aperiam cumque. Recusandae est unde vitae amet qui exercitationem.</p>
			<p class="card-text">
			 <small diff:ignore></small>
			</p>
			<a href:ignore class="btn btn-primary">Read more...</a>
			</div>
			</div>
			<div class="card my-4">
			<div class="card-img" style="background-image: url('https://picsum.photos/1060/300/?image=12');"></div>
			<div class="card-body">
			<h5 diff:ignore class="card-title">Fugiat facere labore quia quia dolor in qui eum tempora.</h5>
			<p diff:ignore class="card-text">Minima atque ex. Esse autem adipisci aut dolores cupiditate possimus.</p>
			<p class="card-text">
			<small diff:ignore></small>
						</p>
			<a href:ignore class="btn btn-primary">Read more...</a>
			</div>
			</div>
			<div class="card my-4">
			<div class="card-img" style="background-image: url('https://picsum.photos/1060/300/?image=12');"></div>
			<div class="card-body">
			<h5 diff:ignore class="card-title">Consectetur quod et quia repellendus adipisci sit ut porro voluptas.</h5>
			<p diff:ignore class="card-text">Id impedit vel quaerat et. Dolores sint dolore in officia.</p>
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
	public void BlogPosts_With_NoData_Should_DisplayNoData_Test()
	{
		// Arrange
		const string expected =
			"""
			<div>
			  <svg class="loading-progress">
			    <circle r="40%" cx="50%" cy="50%"></circle>
			    <circle r="40%" cx="50%" cy="50%"></circle>
			  </svg>
			  <div class="loading-progress-text"></div>
			</div>
			""";

		RegisterServices();

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
	}

	private void RegisterServices()
	{
		Services.AddSingleton(_blogServiceMock.Object);
	}

	private void SetupMocks()
	{
		_blogServiceMock.Setup(m => m.GetAllAsync()).ReturnsAsync(_expectedPosts);
	}
}
