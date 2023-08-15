namespace BlogService.Library.DataAccess;

/// <summary>
///   CollectionNames class
/// </summary>
public static class CollectionNames
{
	/// <summary>
	///   GetCollectionName method
	/// </summary>
	/// <param name="entityName">string</param>
	/// <returns>string collection name</returns>
	public static string GetCollectionName(string entityName)
	{
		return entityName switch
		{
			"BlogPost" => "posts",
			_ => ""
		};
	}
}