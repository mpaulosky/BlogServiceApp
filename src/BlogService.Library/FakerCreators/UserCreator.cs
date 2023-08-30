// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     UserCreator.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.FakerCreators;

/// <summary>
///   FakeUser class
/// </summary>
public static class UserCreator
{
	/// <summary>
	///   Gets a new user.
	/// </summary>
	/// <param name="keepId">bool whether to keep the generated Id</param>
	/// <param name="useNewSeed">bool whether to use a seed other than 0</param>
	/// <returns>User</returns>
	public static User GetNewUser(bool keepId = false, bool useNewSeed = false)
	{
		User user = GenerateFake(useNewSeed).Generate();

		if (!keepId)
		{
			user.Id = string.Empty;
			user.ObjectIdentifier = string.Empty;
			user.Archived = false;
		}

		return user;
	}

	/// <summary>
	///   Gets a list of users.
	/// </summary>
	/// <param name="numberOfUsers">The number of users.</param>
	/// <param name="useNewSeed">bool whether to use a seed other than 0</param>
	/// <returns>A List of Users</returns>
	public static List<User> GetUsers(int numberOfUsers, bool useNewSeed = false)
	{
		List<User> users = GenerateFake(useNewSeed).Generate(numberOfUsers);

		foreach (User user in users.Where(x => x.Archived))
		{
			user.ArchivedBy = new BasicUser(GetNewUser());
		}

		return users;
	}

	/// <summary>
	///   GenerateFake method
	/// </summary>
	/// <param name="useNewSeed">bool whether to use a seed other than 0</param>
	/// <returns>Fake User</returns>
	private static Faker<User> GenerateFake(bool useNewSeed = false)
	{
		var seed = 0;
		if (useNewSeed)
		{
			seed = Random.Shared.Next(10, int.MaxValue);
		}

		return new Faker<User>()
			.RuleFor(x => x.Id, new BsonObjectId(ObjectId.GenerateNewId()).ToString())
			.RuleFor(x => x.ObjectIdentifier, new BsonObjectId(ObjectId.GenerateNewId()).ToString())
			.RuleFor(x => x.FirstName, f => f.Name.FirstName())
			.RuleFor(x => x.LastName, f => f.Name.LastName())
			.RuleFor(x => x.DisplayName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
			.RuleFor(x => x.EmailAddress, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
			.RuleFor(f => f.Archived, f => f.Random.Bool())
			.UseSeed(seed);
	}
}
