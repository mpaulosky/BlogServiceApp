﻿// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : BlogServiceUiNavigator.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI.Tests.Playwright;

/// <summary>
///   A collection of extension methods that navigate the application.
/// </summary>
[ExcludeFromCodeCoverage]
public static class BlogServiceUiNavigator
{
	public static async Task<IPage> GotoIndexPage(this IPage page)
	{
		if (page.Url != "/") await page.GotoAsync("/");

		return page;
	}

	public static async Task<IPage> SearchForHashtag(this IPage page, string hashtag)
	{
		await page.GetByPlaceholder("New Hashtag").FillAsync(hashtag);

		await page.GetByRole(AriaRole.Button, new() { Name = "Add" }).ClickAsync();

		return page;
	}

	public static async Task<IPage> GotoWaterfallPage(this IPage page)
	{
		if (page.Url != "/waterfall") await page.GotoAsync("/waterfall");

		return page;
	}

	public static async Task<IPage> GotoOverlayPage(this IPage page)
	{
		if (page.Url != "/overlay") await page.GotoAsync("/overlay");

		return page;
	}
}
