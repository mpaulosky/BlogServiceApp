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