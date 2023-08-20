// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     MongoUserData.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.DataAccess;

/// <summary>
///   Provides data access to MongoDB for the User model.
/// </summary>
public class MongoUserData : IUserData
{
  private readonly IMongoCollection<User> _users;

  /// <summary>
  ///   MongoUserData constructor
  /// </summary>
  /// <param name="context">IMongoDbContext</param>
  /// <exception cref="ArgumentNullException"></exception>
  public MongoUserData(IMongoDbContextFactory context)
  {
	ArgumentNullException.ThrowIfNull(nameof(context));

	string collectionName = GetCollectionName(nameof(User));

	_users = context.GetCollection<User>(collectionName);
  }

  /// <summary>
  ///   Archives a user asynchronously.
  /// </summary>
  /// <param name="user">The user to archive.</param>
  /// <returns>A Task representing the asynchronous operation.</returns>
  public Task ArchiveAsync(User user)
  {
	var filter = Builders<User>.Filter.Eq("Id", user.Id);
	return _users.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
  }

  /// <summary>
  ///   Retrieves all users asynchronously.
  /// </summary>
  /// <returns>A Task representing the asynchronous operation.</returns>
  public async Task<List<User>> GetAllAsync()
  {
	var results = await _users.FindAsync(_ => true);

	return results.ToList();
  }

  /// <summary>
  ///   Retrieves a user asynchronously by their ID.
  /// </summary>
  /// <param name="id">The ID of the user to retrieve.</param>
  /// <returns>A Task representing the asynchronous operation.</returns>
  public async Task<User> GetAsync(string id)
  {
	var results = await _users.FindAsync(u => u.Id == id);
	return results.FirstOrDefault();
  }

  /// <summary>
  ///   Retrieves a user from authentication asynchronously.
  /// </summary>
  /// <param name="objectId">The object ID of the authenticated user.</param>
  /// <returns>A Task representing the asynchronous operation.</returns>
  public async Task<User> GetFromAuthenticationAsync(string objectId)
  {
	var results = await _users.FindAsync(u => u.ObjectIdentifier == objectId);
	return results.FirstOrDefault();
  }

  /// <summary>
  ///   Creates a user asynchronously.
  /// </summary>
  /// <param name="user">The user to create.</param>
  /// <returns>A Task representing the asynchronous operation.</returns>
  public Task CreateAsync(User user)
  {
	return _users.InsertOneAsync(user);
  }

  /// <summary>
  ///   Updates a user asynchronously.
  /// </summary>
  /// <param name="user">The user to update.</param>
  /// <returns>A Task representing the asynchronous operation.</returns>
  public Task UpdateAsync(User user)
  {
	var filter = Builders<User>.Filter.Eq("Id", user.Id);
	return _users.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
  }
}
