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
			  <div class="card-img">
			    <img diff:ignore>
			  </div>
			  <div class="card-body">
			    <h5 class="card-title">Assumenda iste quia natus et dignissimos reiciendis ad nostrum totam.</h5>
			    <p class="card-text">Voluptatibus doloremque eos asperiores cum ipsam. Pariatur doloribus aperiam cumque non recusandae est unde vitae. Qui exercitationem doloribus facilis.</p>
			    <p class="card-text">
			      <small diff:ignore>Sunday, February 19, 2023</small>
			    </p>
			    <a href="/posts/Moises-Schumm" class="btn btn-primary">Read more...</a>
			  </div>
			</div>
			<div class="card my-4">
			  <div class="card-img">
			<img diff:ignore>
			  </div>
			  <div class="card-body">
			    <h5 class="card-title">Omnis fugiat facere labore quia quia dolor in qui eum.</h5>
			    <p class="card-text">Quae minima atque ex est esse.</p>
			    <p class="card-text">
			      <small diff:ignore>Saturday, February 25, 2023</small>
			    </p>
			    <a href="/posts/Bettye-Crona" class="btn btn-primary">Read more...</a>
			  </div>
			</div>
			<div class="card my-4">
			  <div class="card-img">
			<img diff:ignore>
			  </div>
			  <div class="card-body">
			    <h5 class="card-title">Molestiae aut omnis modi labore et consectetur qui at numquam.</h5>
			    <p class="card-text">Nesciunt dicta dolores nam occaecati deleniti. Distinctio incidunt libero quia debitis autem sequi provident quasi veniam. Et autem porro qui unde dolores dolorem atque sunt sit. Accusamus facilis corporis laboriosam ut molestiae alias.</p>
			    <p class="card-text">
			      <small diff:ignore>Wednesday, January 4, 2023</small>
			    </p>
			    <a href="/posts/Abbie-Mraz" class="btn btn-primary">Read more...</a>
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
