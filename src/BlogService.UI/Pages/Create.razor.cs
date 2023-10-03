// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     Create.razor.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI
// =============================================

using Microsoft.AspNetCore.Components.Forms;

namespace BlogService.UI.Pages;

[UsedImplicitly]
public partial class Create
{
	private readonly BlogPostDto _newBlogPost = new();
	private User _loggedInUser;

	/// <summary>
	///   OnInitializedAsync event
	/// </summary>
	protected override async Task OnInitializedAsync()
	{
		_loggedInUser = await AuthProvider.GetUserFromAuth(UserService);
	}

	private async Task CreateBlogPost()
	{
		var newPost = new BlogPost
		{
			Title = _newBlogPost.Title,
			Url = _newBlogPost.Url,
			Description = _newBlogPost.Description,
			Content = _newBlogPost.Content,
			Author = new BasicUser(_loggedInUser),
			Created = _newBlogPost.Created,
			IsPublished = _newBlogPost.IsPublished,
			Image = _newBlogPost.Image
		};

		await BlogService.CreateAsync(newPost);

		NavigationManager.NavigateTo($"posts/{newPost!.Url}");
	}

	public async Task OnFileChange(InputFileChangeEventArgs e)
	{
		const string format = "image/png";
		var resizeImage = await e.File.RequestImageFileAsync(format, 300, 300);
		var buffer = new byte[resizeImage.Size];
		_ = await resizeImage.OpenReadStream().ReadAsync(buffer);
		var imageData = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
		_newBlogPost.Image = imageData;
	}
}
