// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : RegisterAuthorization.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.UI
// =============================================

namespace BlogService.UI.Registrations;

/// <summary>
///   IServiceCollectionExtensions
/// </summary>
public static partial class ServiceCollectionExtensions
{
	/// <summary>
	///   Add Authorization Services
	/// </summary>
	/// <param name="services">IServiceCollection</param>
	/// <returns>IServiceCollection</returns>
	public static void AddAuthorizationPolicy(this IServiceCollection services)
	{
		services.AddAuthorization(options =>
		{
			options.AddPolicy("Admin", policy =>
			{
				policy.RequireClaim("jobTitle", "Admin");
			});
		});
	}
}
