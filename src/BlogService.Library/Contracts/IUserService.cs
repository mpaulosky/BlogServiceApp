// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     IUserService.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.Contracts;

public interface IUserService
{
	Task ArchiveAsync(User user);

	Task CreateAsync(User user);

	Task<User> GetAsync(string id);

	Task<List<User>> GetAllAsync();

	Task<User> GetByAuthIdAsync(string objectId);

	Task UpdateAsync(User user);
}
