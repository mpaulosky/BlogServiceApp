// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     BlogPosts.razor.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI
// =============================================

namespace BlogService.UI.Shared;

public partial class BlogPosts
{
	private List<BlogPost> Posts { get; set; } = new();

	private const string PlaceHolderImage = "https://picsum.photos/id/201/1060/300";

	protected override async Task OnInitializedAsync()
	{
		Posts = await BlogService.GetAllAsync();
	}
}
