// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GivenAnCreatePage.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.BUnit
// =============================================

namespace BlogService.UI.Pages;

[ExcludeFromCodeCoverage]
public class GivenAnCreatePage : TestContext
{
	private readonly Mock<IBlogPostData> _blogPostDataMock = new();
	private readonly Mock<IUserData> _userDataMock = new();
	private User _expectedUser = new();
	private BlogPost? _expectedPost = new();

	private IRenderedComponent<Create> ComponentUnderTest()
	{
		var component = RenderComponent<Create>();

		return component;
	}

	[Fact]
	public void Create_OnLoad_Should_DisplayPage_Test()
	{
		// Arrange
		const string expectedHtml =
			"""
			<h3>Create a New Blog Post</h3>
			<form >
			  <div class="form-group mb-2">
			    <label for="title">Title</label>
			    <input id="title" class="form-control valid" value=""  >
			  </div>
			  <div class="form-group mb-2">
			    <label for="url">Url</label>
			    <input id="url" class="form-control valid" value=""  >
			  </div>
			  <div class="form-control-file mb-2">
			    <label for="image">Image</label>
			    <input id="image" type="file" >
			  </div>
			  <div class="form-group mb-2">
			    <label for="description">Description</label>
			    <textarea id="description" class="form-control valid" value=""  ></textarea>
			  </div>
			  <div class="form-group mb-2">
			    <label for="content">Content</label>
			    <textarea id="content" class="form-control valid" value=""  ></textarea>
			  </div>
			  <div class="form-group mb-2">
			    <label for="preview">Content Preview</label>
			    <div id="preview" class="form-group mb-2"></div>
			  </div>
			  <div class="form-group mb-2">
			    <label for="date">Date Created</label>
			    <input id="date" type="date" class="form-control valid" value:ignore  >
			  </div>
			  <div class="form-check mb-2">
			    <input id="isPublished" type="checkbox" class="form-check-input valid"  >
			    <label for="isPublished">Publish</label>
			  </div>
			  <button id="submit" type="submit" class="btn btn-primary">Create</button>
			</form>
			""";

		SetupMocks();
		RegisterServices();
		SetAuthenticationAndAuthorization(true, true);

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expectedHtml);
	}

	[Fact]
	public void Create_With_ValidInput_Should_SaveNewBlog_Test()
	{
		// Arrange
		_expectedPost = BlogPostCreator.GetNewBlogPost();

		SetupMocks();
		RegisterServices();
		SetAuthenticationAndAuthorization(true, true);

		// Act
		var cut = ComponentUnderTest();

		cut.Find("#title").Change(_expectedPost.Title);
		cut.Find("#url").Change(_expectedPost.Url);
		cut.Find("#description").Change(_expectedPost.Description);
		cut.Find("#content").Change(_expectedPost.Content);
		cut.Find("#date").Change(DateTime.Now);
		cut.Find("#submit").Click();

		// Assert
		_blogPostDataMock
			.Verify(x =>
				x.CreateAsync(It.IsAny<BlogPost>()), Times.Once);

		var navMan = Services.GetRequiredService<FakeNavigationManager>();

		navMan.Uri.Should().NotBeNull();
		navMan.Uri.Should().Be($"http://localhost/posts/{_expectedPost.Url}");
	}

	[Fact]
	public void Create_With_InValidInput_Should_ShowValidationErrors_Test()
	{
		// Arrange
		const string expectedHtml =
			"""
			<h3>Create a New Blog Post</h3>
			<form >
			  <ul class="validation-errors">
			    <li class="validation-message">The Url field is required.</li>
			    <li class="validation-message">The Title field is required.</li>
			    <li class="validation-message">The Description field is required.</li>
			    <li class="validation-message">The Content field is required.</li>
			  </ul>
			  <div class="form-group mb-2">
			    <label for="title">Title</label>
			    <input id="title" aria-invalid="true" class="form-control invalid" value=""  >
			    <div class="validation-message">The Title field is required.</div>
			  </div>
			  <div class="form-group mb-2">
			    <label for="url">Url</label>
			    <input id="url" aria-invalid="true" class="form-control invalid" value=""  >
			    <div class="validation-message">The Url field is required.</div>
			  </div>
			  <div class="form-control-file mb-2">
			    <label for="image">Image</label>
			    <input id="image" type="file" >
			  </div>
			  <div class="form-group mb-2">
			    <label for="description">Description</label>
			    <textarea id="description" aria-invalid="true" class="form-control invalid" value=""  ></textarea>
			    <div class="validation-message">The Description field is required.</div>
			  </div>
			  <div class="form-group mb-2">
			    <label for="content">Content</label>
			    <textarea id="content" aria-invalid="true" class="form-control invalid" value=""  ></textarea>
			    <div class="validation-message">The Content field is required.</div>
			  </div>
			  <div class="form-group mb-2">
			    <label for="preview">Content Preview</label>
			    <div id="preview" class="form-group mb-2"></div>
			  </div>
			  <div class="form-group mb-2">
			    <label for="date">Date Created</label>
			    <input id="date" type="date" class="form-control valid" value:ignore  >
			  </div>
			  <div class="form-check mb-2">
			    <input id="isPublished" type="checkbox" class="form-check-input valid">
			    <label for="isPublished">Publish</label>
			  </div>
			  <button id="submit" type="submit" class="btn btn-primary">Create</button>
			</form>
			""";

		SetupMocks();
		RegisterServices();
		SetAuthenticationAndAuthorization(true, true);

		// Act
		var cut = ComponentUnderTest();

		cut.Find("#submit").Click();

		// Assert
		cut.MarkupMatches(expectedHtml);
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
