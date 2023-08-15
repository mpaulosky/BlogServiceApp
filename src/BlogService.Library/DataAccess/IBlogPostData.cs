namespace BlogService.Library.DataAccess;

public interface IBlogPostData
{
	Task CreateBlogPost(BlogPost post);
	Task<BlogPost> GetBlogPostAsync(string id);
	Task<BlogPost> GetBlogPostByUrlAsync(string url);
	Task<List<BlogPost>> GetBlogPostsAsync();
	Task UpdateBlogPost(BlogPost post);
}