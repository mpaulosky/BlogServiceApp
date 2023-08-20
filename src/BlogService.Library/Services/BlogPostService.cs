// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     BlogPostService.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.Services;

public class BlogPostService : IBlogService
{
  private readonly IBlogPostData _data;

  /// <summary>
  ///   Initializes a new instance of the <see cref="BlogPostService" /> class with an instance of
  ///   <see cref="IBlogPostData" />.
  /// </summary>
  /// <param name="data">The <see cref="IBlogPostData" /> instance to be used by this class.</param>
  public BlogPostService(IBlogPostData data)
  {
	_data = data;
  }

  /// <summary>
  ///   Archives the specified blog post.
  /// </summary>
  /// <param name="post">The blog post to be archived.</param>
  /// <returns>A task that represents the asynchronous operation.</returns>
  public Task ArchiveAsync(BlogPost post)
  {
	ArgumentNullException.ThrowIfNull(nameof(post));

	return _data.ArchiveAsync(post);
  }

  /// <summary>
  ///   Creates a new blog post.
  /// </summary>
  /// <param name="post">The blog post to be created.</param>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result contains the created
  ///   <see cref="BlogPost" /> instance.
  /// </returns>
  public Task CreateAsync(BlogPost post)
  {
	ArgumentNullException.ThrowIfNull(nameof(post));

	return _data.CreateAsync(post);
  }

  /// <summary>
  ///   Gets all the blog posts.
  /// </summary>
  /// <returns>A task that represents the asynchronous operation. The task result contains a list of all the blog posts.</returns>
  public async Task<List<BlogPost>> GetAllAsync()
  {
	return await _data.GetAllAsync();
  }

  /// <summary>
  ///   Gets the blog post by the specified URL.
  /// </summary>
  /// <param name="url">The URL of the blog post to be retrieved.</param>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result contains the retrieved
  ///   <see cref="BlogPost" /> instance.
  /// </returns>
  public async Task<BlogPost> GetByUrlAsync(string url)
  {
	ArgumentException.ThrowIfNullOrEmpty(nameof(url));
	return await _data.GetByUrlAsync(url);
  }

  /// <summary>
  ///   Updates the specified blog post.
  /// </summary>
  /// <param name="post">The blog post to be updated.</param>
  /// <returns>A task that represents the asynchronous operation.</returns>
  public Task UpdateAsync(BlogPost post)
  {
	ArgumentNullException.ThrowIfNull(nameof(post));
	return _data.UpdateAsync(post);
  }
}
