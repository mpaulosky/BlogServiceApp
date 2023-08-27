// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : GivenAnEntityName.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Unit
// =============================================

namespace BlogService.Library.Tests.Unit.DataAccess;

[ExcludeFromCodeCoverage]
public class GivenAnEntityName
{
	[Theory]
	[InlineData("BlogPost", "posts")]
	[InlineData("User", "users")]
	[InlineData("", "")]
	public void GetCollectionName_ShouldReturnCollectionName(string entityName, string expected)
	{
		var actual = CollectionNames.GetCollectionName(entityName);
		Assert.Equal(expected, actual);
	}
}
