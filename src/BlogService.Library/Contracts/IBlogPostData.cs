// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : IBlogPostData.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library
// =============================================

namespace BlogService.Library.Contracts;

public interface IBlogPostData
{
	Task ArchiveAsync(BlogPost post);

	Task CreateAsync(BlogPost post);

	Task<BlogPost> GetByUrlAsync(string url);

	Task<List<BlogPost>> GetAllAsync();

	Task UpdateAsync(BlogPost post);
}
