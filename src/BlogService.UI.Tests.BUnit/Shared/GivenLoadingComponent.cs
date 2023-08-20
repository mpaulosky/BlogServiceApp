// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     GivenLoadingComponent.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.BUnit
// =============================================

namespace BlogService.UI.Tests.BUnit.Shared;

[ExcludeFromCodeCoverage]
public class GivenLoadingComponent : TestContext
{
	private IRenderedComponent<LoadingComponent> ComponentUnderTest()
	{
		IRenderedComponent<LoadingComponent> component = RenderComponent<LoadingComponent>();

		return component;
	}

	[Fact]
	public void LoadingComponentOnLoad_Test()
	{
		// Arrange
		const string expected =
			"""
			<div>
				<svg class="loading-progress">
					<circle r="40%" cx="50%" cy="50%"></circle>
					<circle r="40%" cx="50%" cy="50%"></circle>
				</svg>
				<div class="loading-progress-text"></div>
			</div>
			""";

		// Act
		var cut = ComponentUnderTest();

		// Assert
		cut.MarkupMatches(expected);
	}
}
