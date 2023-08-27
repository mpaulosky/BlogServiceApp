// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : MyFirstTests.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI.Tests.Playwright.Tests;

[ExcludeFromCodeCoverage]
public class MyFirstTests : TestsBase
{
	public MyFirstTests(PlaywrightFixture webapp, ITestOutputHelper outputHelper) : base(webapp, outputHelper)
	{
	}

	[Fact]
	public async Task CanLoadIndexPage()
	{
		var page = await WebApp.CreatePlaywrightPageAsync();

		await using var trace = await page.TraceAsync("Can Open Index Page", true, true, true);

		page
			.GotoIndexPage().Result
			.TitleAsync().Result.Should().Be("Blazor Blog Home");
	}
}
