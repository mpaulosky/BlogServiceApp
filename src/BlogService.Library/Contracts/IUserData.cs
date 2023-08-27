// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : IUserData.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library
// =============================================

namespace BlogService.Library.Contracts;

public interface IUserData
{
	Task ArchiveAsync(User user);

	Task CreateAsync(User user);

	Task<User> GetAsync(string id);

	Task<User> GetFromAuthenticationAsync(string objectId);

	Task<List<User>> GetAllAsync();

	Task UpdateAsync(User user);
}
