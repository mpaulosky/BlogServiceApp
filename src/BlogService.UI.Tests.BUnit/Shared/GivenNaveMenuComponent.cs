// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : GivenNaveMenuComponent.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.UI.Tests.BUnit
// =============================================

namespace BlogService.UI.Tests.BUnit.Shared;

[ExcludeFromCodeCoverage]
public class GivenNaveMenuComponent : TestContext
{
	private readonly User _expectedUser = UserCreator.GetNewUser(true);

	private IRenderedComponent<NavMenu> ComponentUnderTest()
	{
		IRenderedComponent<NavMenu> component = RenderComponent<NavMenu>();

		return component;
	}

	[Fact]
	public void NavMenuOnLoad_AsNotAuthorized_Test()
	{
		// Arrange
		const string expected =
			"""
			<header >
			<nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3" >
			 <div class="container" >
			   <a class="navbar-brand" asp-area="" asp-page="/Index" >
			     <span >
			       <img height="15" width="15" src="images/icon-192.png" alt="Blazor Img" >
			     </span>Blazor Blog
			   </a>
			   <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation" >
			     <span class="navbar-toggler-icon" ></span>
			   </button>
			   <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between" >
			     <ul class="navbar-nav flex-grow-1" >
			       <li class="nav-item" >
			         <a class="nav-link active" aria-current="page" href="#" >
			           <span class="oi oi-home" aria-hidden="true" ></span>Home
			         </a>
			       </li>
			       <li class="nav-item" >
			         <a class="nav-link" href="MicrosoftIdentity/Account/SignIn" >
			           <span class="oi oi-account-login" aria-hidden="true" ></span>
			           Login
			         </a>
			       </li>
			     </ul>
			   </div>
			 </div>
			</nav>
			</header>
			""";

		SetAuthenticationAndAuthorization(false, false);

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
	}

	[Fact]
	public void NavMenuOnLoad_AsAutorizedNotAdmin_Test()
	{
		// Arrange
		const string expected =
			"""
			<header >
			<nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3" >
			 <div class="container" >
			   <a class="navbar-brand" asp-area="" asp-page="/Index" >
			     <span >
			       <img height="15" width="15" src="images/icon-192.png" alt="Blazor Img" >
			     </span>Blazor Blog
			   </a>
			   <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation" >
			     <span class="navbar-toggler-icon" ></span>
			   </button>
			   <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between" >
			     <ul class="navbar-nav flex-grow-1" >
			       <li class="nav-item" >
			         <a class="nav-link active" aria-current="page" href="#" >
			           <span class="oi oi-home" aria-hidden="true" ></span>Home
			         </a>
			       </li>
			       <li class="nav-item" >
			         <a class="nav-link" href="create" >
			           <span class="oi oi-brush" aria-hidden="true" ></span>Create
			         </a>
			       </li>
			       <li class="nav-item" >
			         <a class="nav-link" href="Profile" >
			           <span class="oi oi-book" aria-hidden="true" ></span>
			           Profile
			         </a>
			       </li>
			       <li class="nav-item" >
			         <a class="nav-link" href="MicrosoftIdentity/Account/SignOut" >
			           <span class="oi oi-account-logout" aria-hidden="true" ></span>
			           Logout
			         </a>
			       </li>
			     </ul>
			   </div>
			 </div>
			</nav>
			</header>
			""";

		SetAuthenticationAndAuthorization(false, true);

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
	}

	[Fact]
	public void NavMenuOnLoad_AsAutorizedAndAdmin_Test()
	{
		// Arrange
		const string expected =
			"""
			<header >
			<nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3" >
			 <div class="container" >
			   <a class="navbar-brand" asp-area="" asp-page="/Index" >
			     <span >
			       <img height="15" width="15" src="images/icon-192.png" alt="Blazor Img" >
			     </span>Blazor Blog
			   </a>
			   <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation" >
			     <span class="navbar-toggler-icon" ></span>
			   </button>
			   <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between" >
			     <ul class="navbar-nav flex-grow-1" >
			       <li class="nav-item" >
			         <a class="nav-link active" aria-current="page" href="#" >
			           <span class="oi oi-home" aria-hidden="true" ></span>Home
			         </a>
			       </li>
			       <li class="nav-item" >
			         <a class="nav-link" href="create" >
			           <span class="oi oi-brush" aria-hidden="true" ></span>Create
			         </a>
			       </li>
			       <li class="nav-item" >
			         <a class="nav-link" href="Admin" >
			           <span class="oi oi-badge" aria-hidden="true" ></span>Admin
			         </a>
			       </li>
			       <li class="nav-item" >
			         <a class="nav-link" href="Profile" >
			           <span class="oi oi-book" aria-hidden="true" ></span>
			           Profile
			         </a>
			       </li>
			       <li class="nav-item" >
			         <a class="nav-link" href="MicrosoftIdentity/Account/SignOut" >
			           <span class="oi oi-account-logout" aria-hidden="true" ></span>
			           Logout
			         </a>
			       </li>
			     </ul>
			   </div>
			 </div>
			</nav>
			</header>
			""";

		SetAuthenticationAndAuthorization(true, true);

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
	}

	private void SetAuthenticationAndAuthorization(bool isAdmin, bool isAuth)
	{
		TestAuthorizationContext authContext = this.AddTestAuthorization();

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
