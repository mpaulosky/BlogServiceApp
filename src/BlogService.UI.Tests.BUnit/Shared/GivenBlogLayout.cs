// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GivenBlogLayout.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.BUnit
// =============================================

namespace BlogService.UI.Shared;

[ExcludeFromCodeCoverage]
public class GivenBlogLayout : TestContext
{
	private readonly User _expectedUser = UserCreator.GetNewUser(true);

	private IRenderedComponent<BlogLayout> ComponentUnderTest()
	{
		var component = RenderComponent<BlogLayout>();

		return component;
	}

	[Fact]
	public void BlogLayoutOnLoad_AsUnAuthorized_Test()
	{
		// Arrange
		const string expected =
			"""
			<div class="container">
			  <header class="fixed-top container d-flex justify-content-between bg-light shadow">
			    <a class="navbar-brand d-flex align-items-center" href="#">
			      <img src="images/icon-192.png" height="20" class="d-inline-block align-top" alt="">
			      <h5 class="m-0 ms-2">Blazor Blog</h5>
			    </a>
			    <ul class="nav me-2 align-items-center">
			      <li>
			        <a class="nav-link text-secondary" href="#">
			          <span class="bi-house-fill">&nbsp;Home</span>
			        </a>
			      </li>
			      <li>
			        <a class="nav-link text-secondary" href="MicrosoftIdentity/Account/SignIn">
			          <span class="bi-box-arrow-in-right">&nbsp;Login</span>
			        </a>
			      </li>
			    </ul>
			  </header>
			  <div id="body" class="content px-4"></div>
			</div>
			""";

		SetAuthenticationAndAuthorization(false, false);

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
	}

	[Fact]
	public void BlogLayoutOnLoad_AsAuthorizedNotAdmin_Test()
	{
		// Arrange
		const string expected =
			"""
			<div class="container">
			  <header class="fixed-top container d-flex justify-content-between bg-light shadow">
			    <a class="navbar-brand d-flex align-items-center" href="#">
			      <img src="images/icon-192.png" height="20" class="d-inline-block align-top" alt="">
			      <h5 class="m-0 ms-2">Blazor Blog</h5>
			    </a>
			    <ul class="nav me-2 align-items-center">
			      <li>
			        <a class="nav-link text-secondary" href="#">
			          <span class="bi-house-fill">&nbsp;Home</span>
			        </a>
			      </li>
			      <li>
			        <a class="nav-link text-secondary" href="create">
			          <span class="bi-brush-fill">&nbsp;Create</span>
			        </a>
			      </li>
			      <li>
			        <a class="nav-link text-secondary" href="Profile">
			          <span class="oi oi-book">&nbsp;Profile</span>
			        </a>
			      </li>
			      <li>
			        <a class="nav-link text-secondary" href="MicrosoftIdentity/Account/SignOut">
			          <span class="bi-box-arrow-left">&nbsp;Logout</span>
			        </a>
			      </li>
			    </ul>
			  </header>
			  <div id="body" class="content px-4"></div>
			</div>
			""";

		SetAuthenticationAndAuthorization(false, true);

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
	}

	[Fact]
	public void BlogLayoutOnLoad_AsAuthorizedAndAdmin_Test()
	{
		// Arrange
		const string expected =
			"""
			<div class="container">
			  <header class="fixed-top container d-flex justify-content-between bg-light shadow">
			    <a class="navbar-brand d-flex align-items-center" href="#">
			      <img src="images/icon-192.png" height="20" class="d-inline-block align-top" alt="">
			      <h5 class="m-0 ms-2">Blazor Blog</h5>
			    </a>
			    <ul class="nav me-2 align-items-center">
			      <li>
			        <a class="nav-link text-secondary" href="#">
			          <span class="bi-house-fill">&nbsp;Home</span>
			        </a>
			      </li>
			      <li>
			        <a class="nav-link text-secondary" href="create">
			          <span class="bi-brush-fill">&nbsp;Create</span>
			        </a>
			      </li>
			      <li>
			        <a class="nav-link text-secondary" href="Profile">
			          <span class="oi oi-book">&nbsp;Profile</span>
			        </a>
			      </li>
			      <li>
			        <a class="nav-link text-secondary" href="Admin">
			          <span class="oi oi-badge">&nbsp;Admin</span>
			        </a>
			      </li>
			      <li>
			        <a class="nav-link text-secondary" href="Edit">
			          <span class="bi-pencil-square">&nbsp;Edit</span>
			        </a>
			      </li>
			      <li>
			        <a class="nav-link text-secondary" href="MicrosoftIdentity/Account/SignOut">
			          <span class="bi-box-arrow-left">&nbsp;Logout</span>
			        </a>
			      </li>
			    </ul>
			  </header>
			  <div id="body" class="content px-4"></div>
			</div>
			""";

		SetAuthenticationAndAuthorization(true, true);

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
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
}
