// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : AllServicesToRegister.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.UI
// =============================================

namespace BlogService.UI.Registrations;

/// <summary>
///   RegisterServices class
/// </summary>
[ExcludeFromCodeCoverage]
public static class AllServicesToRegister
{
	/// <summary>
	///   Configures the services method.
	/// </summary>
	/// <param name="builder">The builder.</param>
	/// <param name="config">ConfigurationManager</param>
	public static void ConfigureServices(this WebApplicationBuilder builder, ConfigurationManager config)
	{
		// Add services to the container.
		builder.Services.RegisterApplicationServices();

		builder.Services.AddAuthorizationPolicy();

		builder.Services.AddAuthentication(config);

		builder.Services.RegisterConnection(config);

		builder.Services.RegisterDataSources();
	}
}
