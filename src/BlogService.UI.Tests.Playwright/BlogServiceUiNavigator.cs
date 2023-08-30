// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     BlogServiceUiNavigator.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI;

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

	public static async Task<IPage> GoToAdminPage(this IPage page)
	{
		if (page.Url != "/admin") await page.GotoAsync("/admin");

		return page;
	}

	public static async Task<IPage> GoToCreatePage(this IPage page)
	{
		if (page.Url != "/create") await page.GotoAsync("/create");

		return page;
	}

	public static async Task<IPage> GoToEditPage(this IPage page)
	{
		if (page.Url != "/edit") await page.GotoAsync("/edit");

		return page;
	}

	public static async Task<IPage> GoToPostPage(this IPage page)
	{
		if (page.Url != "/post") await page.GotoAsync("/post");

		return page;
	}

	public static async Task<IPage> GoToProfilePage(this IPage page)
	{
		if (page.Url != "/profile") await page.GotoAsync("/profile");

		return page;
	}
}
