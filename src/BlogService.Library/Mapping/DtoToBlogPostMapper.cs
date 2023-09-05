// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     DtoToBlogPostMapper.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.Mapping;

public static class DtoToBlogPostMapper
{
	public static BlogPost ToBlogPost(this BlogPostDto post)
	{
		return new BlogPost
		{
			Url = post.Url,
			Title = post.Title,
			Content = post.Content,
			Author = post.Author,
			Description = post.Description,
			Image = post.Image!,
			IsArchived = post.IsArchived,
			Created = post.Created
		};
	}
}
