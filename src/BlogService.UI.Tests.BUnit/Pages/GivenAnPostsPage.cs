﻿// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GivenAnPostsPage.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.BUnit
// =============================================

namespace BlogService.UI.Pages;

[ExcludeFromCodeCoverage]
public class GivenAnPostsPage : TestContext
{
	private readonly Mock<IBlogPostData> _blogPostDataMock = new();
	private readonly Mock<IUserData> _userDataMock = new();
	private User _expectedUser = new();
	private BlogPost? _expectedPost = new();

	private IRenderedComponent<Posts> ComponentUnderTest(string? url)
	{
		var component = RenderComponent<Posts>(parameter =>
		{
			parameter.Add(p => p.Url, url);
		});

		return component;
	}

	[Fact()]
	public void Posts_With_ValidData_Should_DisplayData_Test()
	{
		// Arrange
		const string expected =
			"""
			<div class="post-img">
			  <img diff:ignore>
			</div>
			<h3>Assumenda iste quia natus et dignissimos reiciendis ad nostrum totam.</h3>
			<div>
			  <p>Vel nisi a. Porro sed ad et autem suscipit blanditiis. Modi velit porro dolorum. Adipisci debitis dolorem id vero soluta. Eius saepe eveniet dicta quo ipsum facilis. Eius repellat deleniti et quisquam consequatur aut qui porro.</p>
			  <p>Consectetur voluptatum ut voluptas tempore totam fugiat consequatur autem voluptas. Exercitationem cupiditate nam sit quis qui modi amet quasi fuga. Et molestiae modi non et ut cum explicabo.</p>
			  <p>Est tenetur quos. Cum similique tempora et. Ea pariatur veritatis.</p>
			  <p>A doloribus iure cupiditate minima molestias provident inventore. Dolores nulla laborum aut quod. Recusandae magni cumque et eum nobis. Quia sit est magnam repellat similique veritatis.</p>
			  <p>Similique facilis sint ut beatae excepturi sequi vel rerum. Omnis tempore asperiores corporis et laudantium atque. Quasi incidunt voluptatem ducimus. Sit iure illum sed repudiandae doloremque.</p>
			  <p>Asperiores sit et minima delectus officia est quia quam aut. Aut aliquam magni eveniet dolor aut perspiciatis repellendus excepturi aliquid. Aperiam eligendi quia adipisci dolores culpa voluptatem enim et repellendus. Laborum natus expedita laboriosam. Aspernatur debitis quo.</p>
			  <p>Aliquam voluptatem voluptatum adipisci necessitatibus quos quidem voluptatem. Et asperiores laborum alias voluptatibus. Nesciunt est sunt consectetur veritatis aut minus aut voluptas facilis. Et quo eos ducimus et et praesentium. Omnis molestiae est repellat est. Fuga doloribus officiis.</p>
			  <p>Alias iste tempore vel. Quia repellendus occaecati eum ut cupiditate neque harum omnis. Magnam et praesentium et laboriosam. Voluptatem distinctio quia sed. Est atque aut odio sed eos error.</p>
			  <p>Perferendis similique praesentium asperiores quia quia veniam harum. Aliquam voluptatem perferendis dignissimos exercitationem voluptas qui provident hic. Rerum at eveniet totam. Ex omnis corporis quam reprehenderit nihil nam animi recusandae. Ullam soluta nisi totam et eius voluptates occaecati eos hic.</p>
			  <p>Voluptas amet voluptatem et officia voluptates. Possimus temporibus sed aut laborum. Sunt illum et labore autem deserunt nam vitae. Asperiores nesciunt deleniti veritatis voluptatem maiores eaque.</p>
			</div>
			""";
		_expectedUser = UserCreator.GetNewUser(true);
		_expectedPost = BlogPostCreator.GetBlogPosts(1).ToList().First();

		SetupMocks();
		RegisterServices();
		SetAuthenticationAndAuthorization(true, true);

		// Act
		var cut = ComponentUnderTest(_expectedPost.Url);

		// Assert
		cut.MarkupMatches(expected);
	}

	[Fact()]
	public void Posts_With_NoData_Should_DisplayNoData_Test()
	{
		// Arrange
		const string expected =
			"""
			<div class="post-img">
			  <img src="" alt="image">
			</div>
			<h3></h3>
			<div></div>
			""";
		_expectedUser = UserCreator.GetNewUser(true);
		SetupMocks();
		RegisterServices();
		SetAuthenticationAndAuthorization(true, true);

		// Act
		var cut = ComponentUnderTest(string.Empty);

		// Assert
		cut.MarkupMatches(expected);
	}

	private void SetupMocks()
	{
		_blogPostDataMock.Setup(x => x
				.GetByUrlAsync(It.IsAny<string>()))
			.ReturnsAsync(_expectedPost);

		_userDataMock.Setup(x => x
				.GetFromAuthenticationAsync(It.IsAny<string>()))
			.ReturnsAsync(_expectedUser);
	}

	private void SetAuthenticationAndAuthorization(bool isAdmin, bool isAuth)
	{
		var authContext = this.AddTestAuthorization();

		if (isAuth)
		{
			authContext.SetAuthorized(_expectedUser.DisplayName);
			authContext.SetClaims(
				new Claim("objectidentifier", _expectedUser.Id)
			);
		}

		if (isAdmin)
		{
			authContext.SetPolicies("Admin");
		}
	}

	private void RegisterServices()
	{
		Services.AddSingleton<IBlogService>(new BlogPostService(_blogPostDataMock.Object));
		Services.AddSingleton<IUserService>(new UserService(_userDataMock.Object));
	}
}
