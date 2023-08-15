// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     IBlogPostData.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.DataAccess;

public interface IBlogPostData
{
	Task CreateBlogPost(BlogPost post);
	Task<BlogPost> GetBlogPostAsync(string id);
	Task<BlogPost> GetBlogPostByUrlAsync(string url);
	Task<List<BlogPost>> GetBlogPostsAsync();
	Task UpdateBlogPost(BlogPost post);
}
