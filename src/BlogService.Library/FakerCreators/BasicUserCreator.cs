// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : BasicUserCreator.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library
// =============================================

namespace BlogService.Library.FakerCreators;

public class BasicUserCreator
{
	/// <summary>
	///   Gets a new basic user.
	/// </summary>
	/// <param name="keepId">bool whether to keep the generated Id</param>
	/// <param name="useNewSeed">bool whether to use a seed other than 0</param>
	/// <returns>BasicUser</returns>
	public static BasicUser GetNewBasicUser(bool keepId = false, bool useNewSeed = false)
	{
		BasicUser user = GenerateFake(useNewSeed).Generate();

		if (!keepId)
		{
			user.Id = string.Empty;
		}

		return user;
	}

	/// <summary>
	///   Gets a list of basic users.
	/// </summary>
	/// <param name="numberOfUsers">The number of users.</param>
	/// <param name="useNewSeed">bool whether to use a seed other than 0</param>
	/// <returns>A List of BasicUsers</returns>
	public static List<BasicUser> GetBasicUsers(int numberOfUsers, bool useNewSeed = false)
	{
		List<BasicUser> users = GenerateFake(useNewSeed).Generate(numberOfUsers);

		return users;
	}

	/// <summary>
	///   GenerateFake method
	/// </summary>
	/// <param name="useNewSeed">bool whether to use a seed other than 0</param>
	/// <returns>Fake BasicUser</returns>
	private static Faker<BasicUser> GenerateFake(bool useNewSeed = false)
	{
		var seed = 0;
		if (useNewSeed)
		{
			seed = Random.Shared.Next(10, int.MaxValue);
		}

		return new Faker<BasicUser>()
			.RuleFor(x => x.Id, new BsonObjectId(ObjectId.GenerateNewId()).ToString())
			.RuleFor(x => x.FirstName, f => f.Name.FirstName())
			.RuleFor(x => x.LastName, f => f.Name.LastName())
			.RuleFor(x => x.DisplayName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
			.RuleFor(x => x.EmailAddress, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
			.UseSeed(seed);
	}
}
