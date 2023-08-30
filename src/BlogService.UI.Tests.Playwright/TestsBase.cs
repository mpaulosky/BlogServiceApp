// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     TestsBase.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI.Tests.Playwright;

// By default all pages are created in the same browser instance (and may share cookie states etc.) for the scope of the fixture, but will use different 'tabs'.
// The fixture is scoped to each derived Tests Class.
// If the tests in a class need to be separated from each other (e.g. testing auth scenarios), then use incognito contexts
// If required you can launch a new browser instance
[ExcludeFromCodeCoverage]
public abstract class TestsBase : IClassFixture<PlaywrightFixture>
{
	protected readonly PlaywrightFixture WebApp;
	protected readonly ITestOutputHelper OutputHelper;

	protected TestsBase(PlaywrightFixture webapp, ITestOutputHelper outputHelper)
	{
		WebApp = webapp;
		OutputHelper = outputHelper;
	}
}
