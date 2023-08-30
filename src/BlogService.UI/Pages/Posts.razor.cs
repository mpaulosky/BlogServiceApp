// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     Posts.razor.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI
// =============================================

namespace BlogService.UI.Pages;

[UsedImplicitly]
public partial class Posts
{
	private BlogPost _post;
	private const string PlaceHolderImage = "https://picsum.photos/id/201/1060/300";

	[Parameter] public string Url { get; set; }

	protected override async Task OnInitializedAsync()
	{
		_post = await BlogService.GetByUrlAsync(Url ?? throw new InvalidOperationException());
	}
}
