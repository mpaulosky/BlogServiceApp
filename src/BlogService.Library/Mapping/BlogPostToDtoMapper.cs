// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     BlogPostToDtoMapper.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.Mapping;

public static class BlogPostToDtoMapper
{
	public static BlogPostDto ToBlogPostDto(this BlogPost post)
	{
		return new BlogPostDto
		{
			Url = post.Url,
			Title = post.Title,
			Content = post.Content,
			Author = post.Author,
			Description = post.Description,
			Image = post.Image!,
			IsDeleted = post.IsDeleted,
			Created = post.Created
		};
	}
}
