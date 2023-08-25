// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : StubBlogPostService.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.UI.Tests.Playwright
// =============================================

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlogService.UI.Tests.Playwright;

public class StubBlogPostService : IBlogService
{
	public string Id { get; } = "TEST";
	public string DisplayName { get; } = "TEST";
	public TimeSpan NewContentRetrievalFrequency => TimeSpan.FromMilliseconds(1000);

	public Task<IEnumerable<BlogPost>> GetContent(DateTimeOffset since)
	{
		return Task.FromResult(BlogPostCreator.GetBlogPosts(3));
	}

	public async Task ArchiveAsync(BlogPost post)
	{
		throw new NotImplementedException();
	}

	public async Task CreateAsync(BlogPost post)
	{
		throw new NotImplementedException();
	}

	public async Task<List<BlogPost>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public async Task<BlogPost> GetByUrlAsync(string url)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateAsync(BlogPost post)
	{
		throw new NotImplementedException();
	}
}

public static class StubBlogPostServiceExtensions
{
	public static IServiceCollection UseOnlyTestContainerConnections(this IServiceCollection services)
	{
		ServiceDescriptor? dbConnectionDescriptor =
			services.SingleOrDefault(d => d.ServiceType == typeof(IMongoDbContextFactory));

		services.Remove(dbConnectionDescriptor!);

		ServiceDescriptor? dbSettings =
			services.SingleOrDefault(d => d.ServiceType == typeof(IDatabaseSettings));

		services.Remove(dbSettings!);

		services.AddSingleton<IBlogService, StubBlogPostService>();
		return services;
	}

	public static IHostBuilder UseOnlyTestContainer(this IHostBuilder builder) =>
		builder.ConfigureServices(services => services.UseOnlyTestContainerConnections());
}

public class StartStubBlogPostService
{
	public IServiceCollection RegisterServices(IServiceCollection services)
	{
		services.AddSingleton<IBlogService, StubBlogPostService>();

		return services;
	}
}
