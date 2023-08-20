// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     DatabaseSettingsTests.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : IssueTracker
// Project Name :  IssueTracker.CoreBusiness.Tests.Unit
// =============================================

namespace BlogService.Library.Tests.Unit.Models;

[ExcludeFromCodeCoverage]
public class DatabaseSettingsTests
{
	[Fact(DisplayName = "CreateDatabaseSettings")]
	public void DatabaseSettings_With_Valid_Data_Should_SetThe_Values_Test()
	{
		// Arrange
		const string expectedCs = "ConnectionString";
		const string expectedDbName = "DatabaseName";

		// Act
		var result = new DatabaseSettings(expectedCs, expectedDbName);

		// Assert
		result.ConnectionStrings.Should().Be(expectedCs);
		result.DatabaseName.Should().Be(expectedDbName);
	}

	[Fact(DisplayName = "GetDatabaseSettings")]
	public void DatabaseSettings_Set_With_Valid_Data_Should_Return_Valid_Data_Test()
	{
		// Arrange
		const string expectedCs = "ConnectionString";
		const string expectedDbName = "DatabaseName";

		var result = new DatabaseSettings(expectedCs, expectedDbName);

		// Act
		string valConn = result.ConnectionStrings;
		string valDbName = result.DatabaseName;

		// Assert
		valConn.Should().Be(expectedCs);
		valDbName.Should().Be(expectedDbName);
	}
}
