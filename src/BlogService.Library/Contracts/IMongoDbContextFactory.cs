// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     IMongoDbContextFactory.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.Contracts;

public interface IMongoDbContextFactory
{
	string ConnectionString { get; }

	string DbName { get; }

	IMongoDatabase Database { get; }

	IMongoCollection<T> GetCollection<T>(string name);

	MongoClient Client { get; }
}
