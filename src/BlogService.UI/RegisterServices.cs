// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     RegisterServices.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI
// =============================================

using BlogService.Library.DataAccess;

namespace BlogService.UI;

public static class RegisterServices
{
	public static void ConfigureServices(this WebApplicationBuilder builder, IConfiguration configuration)
	{
		// Add services to the container.
		builder.Services.AddRazorPages();
		builder.Services.AddServerSideBlazor();
		builder.Services.AddMemoryCache();
		builder.Services.AddSingleton<IDbConnection, DbConnection>();
		builder.Services.AddSingleton<IBlogPostData, MongoBlogPostData>();
		builder.Services.AddSingleton<IUserData, MongoUserData>();
	}
}
