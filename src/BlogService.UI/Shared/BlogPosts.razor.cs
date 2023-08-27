// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : BlogPosts.razor.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.UI
// =============================================

namespace BlogService.UI.Shared;

public partial class BlogPosts
{
	private List<BlogPost> Posts { get; set; } = new();

	const string PlaceHolderImage = "https://via.placeholder.com/1060x180";

	protected override async Task OnInitializedAsync()
	{
		Posts = await BlogService.GetAllAsync();
	}
}
