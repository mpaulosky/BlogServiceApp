// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     RegisterDatabase.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI
// =============================================

namespace BlogService.UI.Registrations;

public static class RegisterDatabase
{
  public static void RegisterDataSources(this IServiceCollection services)
  {
	// Add services to the container.
	services.AddSingleton<IMongoDbContextFactory, MongoDbContextFactory>();
	services.AddSingleton<IBlogPostData, MongoBlogPostData>();
	services.AddSingleton<IUserData, MongoUserData>();
	services.AddSingleton<IBlogService, BlogPostService>();
	services.AddSingleton<IUserService, UserService>();
  }
}
