// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     IBlogService.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.Contracts;

public interface IBlogService
{
	Task ArchiveAsync(BlogPost post);

	Task CreateAsync(BlogPost post);

	Task<List<BlogPost>> GetAllAsync();

	Task<BlogPost> GetByUrlAsync(string url);

	Task UpdateAsync(BlogPost post);
}
