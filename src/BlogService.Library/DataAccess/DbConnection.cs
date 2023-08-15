// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     DbConnection.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.DataAccess;

public class DbConnection : IDbConnection
{
	private readonly string _connectionId = "MongoDb";

	public DbConnection(IConfiguration configuration)
	{
		var config = configuration;
		Client = new MongoClient(config.GetConnectionString(_connectionId));
		DbName = config["DatabaseName"];
		var database = Client.GetDatabase(DbName);

		UserCollection = database.GetCollection<User>(UserCollectionName);
		BlogPostCollection = database.GetCollection<BlogPost>(BlogCollectionName);
	}

	public string DbName { get; private set; }
	public string UserCollectionName { get; private set; } = CollectionNames.GetCollectionName("User");
	public string BlogCollectionName { get; private set; } = CollectionNames.GetCollectionName("BlogPost");
	public MongoClient Client { get; private set; }
	public IMongoCollection<User> UserCollection { get; private set; }
	public IMongoCollection<BlogPost> BlogPostCollection { get; private set; }
}
