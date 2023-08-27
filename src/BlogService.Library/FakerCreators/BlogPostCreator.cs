// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : BlogPostCreator.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library
// =============================================

namespace BlogService.Library.FakerCreators;

public static class BlogPostCreator
{
	/// <summary>
	///   Gets a new post.
	/// </summary>
	/// <param name="keepId">bool whether to keep the generated Id</param>
	/// <param name="useNewSeed">bool whether to use a seed other than 0</param>
	/// <returns>BlogPost</returns>
	public static BlogPost GetNewBlogPost(bool keepId = false, bool useNewSeed = false)
	{
		var post = GenerateFake(useNewSeed).Generate();

		if (!keepId)
		{
			post.Id = string.Empty;
		}

		return post;
	}

	public static List<BlogPost> GetNewBlogPosts(int numberOfPosts)
	{
		var posts = GenerateFake().Generate(numberOfPosts);

		foreach (var post in posts)
		{
			post.Id = string.Empty;
		}

		return posts;
	}

	/// <summary>
	///   Gets a list of posts.
	/// </summary>
	/// <param name="numberOfPosts">The number of posts.</param>
	/// <param name="useNewSeed">bool whether to use a seed other than 0</param>
	/// <returns>A List of BlogPost</returns>
	public static IEnumerable<BlogPost> GetBlogPosts(int numberOfPosts, bool useNewSeed = false)
	{
		var posts = GenerateFake(useNewSeed).Generate(numberOfPosts);

		return posts;
	}

	/// <summary>
	///   GenerateFake method
	/// </summary>
	/// <param name="useNewSeed">bool whether to use a seed other than 0</param>
	/// <returns>Fake BlogPost</returns>
	private static Faker<BlogPost> GenerateFake(bool useNewSeed = false)
	{
		var seed = 0;
		if (useNewSeed)
		{
			seed = Random.Shared.Next(10, int.MaxValue);
		}

		return new Faker<BlogPost>()
			.RuleFor(x => x.Id, new BsonObjectId(ObjectId.GenerateNewId()).ToString())
			.RuleFor(x => x.Url, f => $"{f.Name.FirstName()}-{f.Name.LastName()}")
			.RuleFor(c => c.Title, f => f.Lorem.Sentence(10))
			.RuleFor(x => x.Description, f => f.Lorem.Paragraph(1))
			.RuleFor(x => x.Content, f => f.Lorem.Paragraphs(10))
			.RuleFor(x => x.Author, f => f.Name.FullName())
			.RuleFor(x => x.Created, f => f.Date.Past())
			.RuleFor(x => x.Updated, f => f.Date.Past())
			.RuleFor(x => x.IsPublished, f => f.Random.Bool())
			.RuleFor(x => x.IsDeleted, f => f.Random.Bool())
			.RuleFor(x => x.Image, f => f.Image.PicsumUrl(1060, 300, false, false, 201))
			.UseSeed(seed);
	}
}
