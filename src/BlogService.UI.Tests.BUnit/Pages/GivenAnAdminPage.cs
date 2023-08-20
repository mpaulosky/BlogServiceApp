// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GivenAnAdminPage.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.BUnit
// =============================================

namespace BlogService.UI.Tests.BUnit.Pages;

[ExcludeFromCodeCoverage]
public class GivenAnAdminPage : TestContext
{
  private readonly Mock<IUserData> _userDataMock = new();
  private User _expectedUser = new();

  private IRenderedComponent<Admin> ComponentUnderTest()
  {
	IRenderedComponent<Admin> component = RenderComponent<Admin>();

	return component;
  }

  [Fact()]
  public void Admin_With_ValidData_Should_DisplayData_Test()
  {
	// Arrange
	const string expected =
		"""
			<h3>Admin</h3>
			<div class="form-layout mb-3">
			<div class="close-button-section">
			 <button id="close-page" class="btn btn-close" ></button>
			</div>
			<div class="form-layout mb-3"></div>
			</div>
			""";

	_expectedUser = UserCreator.GetNewUser(true);

	SetupMocks();
	RegisterServices();
	SetAuthenticationAndAuthorization(true, true);

	// Act
	var cut = ComponentUnderTest();

	// Assert
	cut.MarkupMatches(expected);
  }

  [Fact()]
  public void Admin_With_NoData_Should_DisplayNoData_Test()
  {
	// Arrange
	const string expected =
		"""
			<h3>Admin</h3>
			<div class="form-layout mb-3">
			<div class="close-button-section">
			 <button id="close-page" class="btn btn-close" ></button>
			</div>
			<div class="form-layout mb-3"></div>
			</div>
			""";

	SetupMocks();
	RegisterServices();
	SetAuthenticationAndAuthorization(true, true);

	// Act
	var cut = ComponentUnderTest();

	// Assert
	cut.MarkupMatches(expected);
  }

  private void SetupMocks()
  {
	_userDataMock.Setup(x => x
			.GetFromAuthenticationAsync(It.IsAny<string>()))
		.ReturnsAsync(_expectedUser);
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

  private void RegisterServices()
  {
	Services.AddSingleton<IUserService>(new UserService(_userDataMock.Object));
  }
}
