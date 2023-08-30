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
