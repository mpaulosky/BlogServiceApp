// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GivenAnIndexPage.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.BUnit
// =============================================

namespace BlogService.UI.Pages;

[ExcludeFromCodeCoverage]
public class GivenAnIndexPage : TestContext
{
	private readonly Mock<IBlogPostData> _blogPostDataMock = new();
	private List<BlogPost>? _expectedPosts = new();

	private IRenderedComponent<Index> ComponentUnderTest()
	{
		var component = RenderComponent<Index>();

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
			      <small diff:ignore>Sunday, May 7, 2023</small>
			    </p>
			    <a diff:ignore>Read more...</a>
			  </div>
			</div>
			<div class="card my-4">
			  <div class="card-img">
			    <img diff:ignore>
			  </div>
			  <div class="card-body">
			    <h5 class="card-title">Fugit nesciunt omnis fugiat facere labore quia quia dolor in.</h5>
			    <p class="card-text">Tempora in quae minima atque ex est esse autem. Aut dolores cupiditate possimus. Quisquam blanditiis dicta qui ipsa.</p>
			    <p class="card-text">
			      <small diff:ignore>Wednesday, May 31, 2023</small>
			    </p>
			    <a diff:ignore>Read more...</a>
			  </div>
			</div>
			<div class="card my-4">
			  <div class="card-img">
			    <img diff:ignore>
			  </div>
			  <div class="card-body">
			    <h5 class="card-title">Dicta aut quas quaerat soluta dignissimos alias dolorum molestiae aut.</h5>
			    <p class="card-text">Labore et consectetur qui. Numquam et dolores nesciunt dicta dolores nam occaecati deleniti. Distinctio incidunt libero quia debitis autem sequi provident quasi veniam. Et autem porro qui unde dolores dolorem atque sunt sit.</p>
			    <p class="card-text">
			      <small diff:ignore>Saturday, July 8, 2023</small>
			    </p>
			    <a diff:ignore>Read more...</a>
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
