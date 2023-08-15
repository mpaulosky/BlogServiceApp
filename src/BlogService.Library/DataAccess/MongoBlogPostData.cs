// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     MongoBlogPostData.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.DataAccess;

public class MongoBlogPostData : IBlogPostData
{
	private readonly IMongoCollection<BlogPost> _posts;

	public MongoBlogPostData(IDbConnection db)
	{
		_posts = db.BlogPostCollection;
	}

	public async Task<List<BlogPost>> GetBlogPostsAsync()
	{
		var results = await _posts.FindAsync(_ => true);

		return results.ToList();
	}

	public async Task<BlogPost> GetBlogPostAsync(string id)
	{
		var results = await _posts.FindAsync(u => u.Id == id);
		return results.FirstOrDefault();
	}

	public async Task<BlogPost> GetBlogPostByUrlAsync(string url)
	{
		var results = await _posts.FindAsync(u => u.Url == url);
		return results.FirstOrDefault();
	}

	public Task CreateBlogPost(BlogPost post)
	{
		return _posts.InsertOneAsync(post);
	}

	public Task UpdateBlogPost(BlogPost post)
	{
		var filter = Builders<BlogPost>.Filter.Eq("Id", post.Id);
		return _posts.ReplaceOneAsync(filter, post, new ReplaceOptions { IsUpsert = true });
	}
}
