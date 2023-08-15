// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     MongoUserData.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.DataAccess;

public class MongoUserData : IUserData
{
	private readonly IMongoCollection<User> _users;

	public MongoUserData(IDbConnection db)
	{
		_users = db.UserCollection;
	}

	public async Task<List<User>> GetUsersAsync()
	{
		var results = await _users.FindAsync(_ => true);

		return results.ToList();
	}

	public async Task<User> GetUserAsync(string id)
	{
		var results = await _users.FindAsync(u => u.Id == id);
		return results.FirstOrDefault();
	}

	public async Task<User> GetUserFromAuthenticationAsync(string objectId)
	{
		var results = await _users.FindAsync(u => u.ObjectIdentifier == objectId);
		return results.FirstOrDefault();
	}

	public Task CreateUser(User user)
	{
		return _users.InsertOneAsync(user);
	}

	public Task UpdateUser(User user)
	{
		var filter = Builders<User>.Filter.Eq("Id", user.Id);
		return _users.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
	}
}
