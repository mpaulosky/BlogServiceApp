// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : SampleData.razor.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.UI
// =============================================

namespace BlogService.UI.Pages;

[UsedImplicitly]
public partial class SampleData
{
	private bool _usersCreated;
	private bool _blogsCreated;

	private async Task CreateUsers()
	{
		var users = await UserData.GetAllAsync();
		if (users?.Count > 0)
		{
			_usersCreated = true;
			return;
		}

		User user = new()
		{
			FirstName = "Matthew",
			LastName = "Paulosky",
			DisplayName = "mpaulosky",
			EmailAddress = "matthew.paulosky@outlook.com"
		};
		await UserData.CreateAsync(user);
		_usersCreated = true;
	}

	private async Task CreateBlogPosts()
	{
		var posts = await BlogPostData.GetAllAsync();
		if (posts?.Count > 0)
		{
			_blogsCreated = true;
			return;
		}

		BlogPost post = new()
		{
			Author = "mpaulosky",
			Content = "Test Content",
			Description = "Test Description",
			Title = "Test Title",
			Created = DateTime.UtcNow
		};
		await BlogPostData.CreateAsync(post);
		_blogsCreated = true;
	}
}
