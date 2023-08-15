namespace BlogService.Library.DataAccess;

public class DbConnection : IDbConnection
{
	private readonly IConfiguration _config;
	private readonly IMongoDatabase _database;
	private string _connectionId = "MongoDb";
	public string DbName { get; private set; }
	public string UserCollectionName { get; private set; } = "users";
	public string BlogCollectionName { get; private set; } = "posts";
	public MongoClient Client { get; private set; }
	public IMongoCollection<User> UserCollection { get; private set; }
	public IMongoCollection<BlogPost> BlogPostCollection { get; private set; }

	public DbConnection(IConfiguration configuration)
	{
		_config = configuration;
		Client = new MongoClient(_config.GetConnectionString(_connectionId));
		DbName = _config["DatabaseName"];
		_database = Client.GetDatabase(DbName);

		UserCollection = _database.GetCollection<User>(UserCollectionName);
		BlogPostCollection = _database.GetCollection<BlogPost>(BlogCollectionName);
	}
}