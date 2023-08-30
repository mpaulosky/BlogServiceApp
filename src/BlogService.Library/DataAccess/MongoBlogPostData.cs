// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     MongoBlogPostData.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.DataAccess;

/// <summary>
///   Data access implementation for MongoDB with BlogPost collection
/// </summary>
public class MongoBlogPostData : IBlogPostData
{
	private readonly IMongoCollection<BlogPost> _posts;

	/// <summary>
	///   MongoBlogPostData constructor
	/// </summary>
	/// <param name="context">IMongoDbContext</param>
	/// <exception cref="ArgumentNullException"></exception>
	public MongoBlogPostData(IMongoDbContextFactory context)
	{
		ArgumentNullException.ThrowIfNull(nameof(context));

		string collectionName = GetCollectionName(nameof(BlogPost));

		_posts = context.GetCollection<BlogPost>(collectionName);
	}

	/// <summary>
	///   Archives a BlogPost async
	/// </summary>
	/// <param name="post">The BlogPost object to be archived</param>
	/// <returns>A task placeholder for the async operation</returns>
	public Task ArchiveAsync(BlogPost post)
	{
		var filter = Builders<BlogPost>.Filter.Eq("Id", post.Id);
		return _posts.ReplaceOneAsync(filter, post, new ReplaceOptions { IsUpsert = true });
	}

	/// <summary>
	///   Creates a BlogPost async
	/// </summary>
	/// <param name="post">The BlogPost object to be created</param>
	/// <returns>A task placeholder for the async operation</returns>
	public Task CreateAsync(BlogPost post)
	{
		return _posts.InsertOneAsync(post);
	}

	/// <summary>
	///   Gets all BlogPost objects async
	/// </summary>
	/// <returns>A List of all the BlogPost objects</returns>
	public async Task<List<BlogPost>> GetAllAsync()
	{
		var results = await _posts.FindAsync(_ => true);

		return results.ToList();
	}

	/// <summary>
	///   Gets a BlogPost object by looking up its URL async
	/// </summary>
	/// <param name="url">The URL of the BlogPost to look up</param>
	/// <returns>The BlogPost corresponding to the URL provided or null if it doesn't exist</returns>
	public async Task<BlogPost> GetByUrlAsync(string url)
	{
		var results = await _posts.FindAsync(u => u.Url == url);
		return results.FirstOrDefault();
	}

	/// <summary>
	///   Updates a BlogPost asynchronously
	/// </summary>
	/// <param name="post">The BlogPost object to be updated</param>
	/// <returns>A task placeholder for the async operation</returns>
	public Task UpdateAsync(BlogPost post)
	{
		var filter = Builders<BlogPost>.Filter.Eq("Id", post.Id);
		return _posts.ReplaceOneAsync(filter, post, new ReplaceOptions { IsUpsert = true });
	}
}
