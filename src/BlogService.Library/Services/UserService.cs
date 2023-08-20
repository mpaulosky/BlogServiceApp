// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     UserService.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.Services;

public class UserService : IUserService
{
  private readonly IUserData _data;

  /// <summary>
  ///   Initializes a new instance of the <see cref="UserService" /> class with an instance of <see cref="IUserData" />.
  /// </summary>
  /// <param name="data">The <see cref="IUserData" /> instance to be used by this class.</param>
  public UserService(IUserData data)
  {
	_data = data;
  }

  /// <summary>
  ///   Archives the specified user.
  /// </summary>
  /// <param name="user">The user to be archived.</param>
  /// <returns>A task that represents the asynchronous operation.</returns>
  public Task ArchiveAsync(User user)
  {
	ArgumentNullException.ThrowIfNull(nameof(user));

	return _data.ArchiveAsync(user);
  }

  /// <summary>
  ///   Creates a new user.
  /// </summary>
  /// <param name="user">The user to be created.</param>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result contains the created <see cref="User" />
  ///   instance.
  /// </returns>
  public Task CreateAsync(User user)
  {
	ArgumentNullException.ThrowIfNull(nameof(user));

	return _data.CreateAsync(user);
  }

  /// <summary>
  ///   Retrieves a user asynchronously by their ID.
  /// </summary>
  /// <param name="id">The ID of the user to retrieve.</param>
  /// <returns>A Task representing the asynchronous operation.</returns>
  public async Task<User> GetAsync(string id)
  {
	ArgumentException.ThrowIfNullOrEmpty(nameof(id));
	return await _data.GetAsync(id);
  }

  /// <summary>
  ///   Gets all the users.
  /// </summary>
  /// <returns>A task that represents the asynchronous operation. The task result contains a list of all the users.</returns>
  public async Task<List<User>> GetAllAsync()
  {
	return await _data.GetAllAsync();
  }

  public async Task<User> GetByAuthIdAsync(string objectId)
  {
	ArgumentException.ThrowIfNullOrEmpty(nameof(objectId));
	return await _data.GetFromAuthenticationAsync(objectId);
  }

  /// <summary>
  ///   Updates the specified user.
  /// </summary>
  /// <param name="user">The user to be updated.</param>
  /// <returns>A task that represents the asynchronous operation.</returns>
  public Task UpdateAsync(User user)
  {
	ArgumentNullException.ThrowIfNull(nameof(user));
	return _data.UpdateAsync(user);
  }
}
