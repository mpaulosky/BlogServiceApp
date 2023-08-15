// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     IDbConnection.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.DataAccess;

public interface IDbConnection
{
	string BlogCollectionName { get; }
	IMongoCollection<BlogPost> BlogPostCollection { get; }
	MongoClient Client { get; }
	string DbName { get; }
	IMongoCollection<User> UserCollection { get; }
	string UserCollectionName { get; }
}
