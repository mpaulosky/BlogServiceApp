﻿// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     RegisterAuthorization.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : IssueTracker
// Project Name :  IssueTracker.UI
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
	public static IServiceCollection AddAuthorizationPolicy(this IServiceCollection services)
	{
		services.AddAuthorization(options =>
		{
			options.AddPolicy("Admin", policy =>
			{
				policy.RequireClaim("jobTitle", "Admin");
			});
		});

		return services;
	}
}