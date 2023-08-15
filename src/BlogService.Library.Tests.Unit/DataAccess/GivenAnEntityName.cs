using BlogService.Library.DataAccess;

namespace BlogService.Library.Tests.Unit.DataAccess;

public class GivenAnEntityName
{
	[Theory]
	[InlineData("BlogPost", "posts")]
	[InlineData("", "")]
	public void GetCollectionName_ShouldReturnCollectionName(string entityName, string expected)
	{
		var actual = CollectionNames.GetCollectionName(entityName);
		Assert.Equal(expected, actual);
	}
}