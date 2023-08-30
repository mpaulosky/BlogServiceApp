// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     MyFirstTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI.Pages;

[ExcludeFromCodeCoverage]
public class PageTests : TestsBase
{
	public PageTests(PlaywrightFixture webapp, ITestOutputHelper outputHelper) : base(webapp, outputHelper)
	{
	}

	[Fact]
	public async Task CheckHomePageTitle()
	{
		// Arrange
		var page = await WebApp.CreatePlaywrightPageAsync();

		// Act
		await page.GotoIndexPage();

		var result = await page.TitleAsync();

		// Assert
		result.Should().Be("Blazor Blog Home");

		await page.CloseAsync();
	}
}
