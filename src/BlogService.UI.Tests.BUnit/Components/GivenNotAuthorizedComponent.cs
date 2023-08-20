// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GivenNotAuthorizedComponent.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.BUnit
// =============================================

using BlogService.UI.Components;

namespace BlogService.UI.Tests.BUnit.Components;

[ExcludeFromCodeCoverage]
public class GivenNotAuthorizedComponent : TestContext
{
  private IRenderedComponent<NotAuthorized> ComponentUnderTest()
  {
	IRenderedComponent<NotAuthorized> component = RenderComponent<NotAuthorized>();

	return component;
  }

  [Fact]
  public void LoadingComponentOnLoad_Test()
  {
	// Arrange
	const string expected =
		"""
			<div class="row justify-content-center">
			<div class="col-xl-8 col-lg-10 form-layout">
			 <div class="row">
			   <div class="col-11">
			     <div class="fw-bold mb-2 fs-5">Authorization Required</div>
			     <p>
			       You are not authorized to access this section. You need to be logged in 					to submit new issues. You need to be an admin to manage the issues.
			     </p>
			   </div>
			   <div class="col-1 close-button-section">
			     <button class="btn btn-close" ></button>
			   </div>
			 </div>
			</div>
			</div>
			""";

	// Act
	var cut = ComponentUnderTest();

	// Assert
	cut.MarkupMatches(expected);
  }

  [Fact]
  public void LoadingComponentClickCloseButtonShouldNavigateToHome_Test()
  {
	// Arrange
	const string expectedUrl = "http://localhost/";

	// Act
	var cut = ComponentUnderTest();
	cut.Find("button").Click();

	// Assert
	var navMan = Services.GetRequiredService<FakeNavigationManager>();
	navMan.Uri.Should().NotBeNull();
	navMan.Uri.Should().Be(expectedUrl);
  }
}
