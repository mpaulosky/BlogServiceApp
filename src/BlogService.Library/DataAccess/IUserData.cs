// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     IUserData.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.DataAccess;

public interface IUserData
{
	Task CreateUser(User user);
	Task<User> GetUserAsync(string id);
	Task<User> GetUserFromAuthenticationAsync(string objectId);
	Task<List<User>> GetUsersAsync();
	Task UpdateUser(User user);
}
