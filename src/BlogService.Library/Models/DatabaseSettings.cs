// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     DatabaseSettings.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.Library
// =============================================

namespace BlogService.Library.Models;

/// <summary>
///   DatabaseSettings class
/// </summary>
public class DatabaseSettings : IDatabaseSettings
{
  public DatabaseSettings(string connectionStrings, string databaseName)
  {
	ConnectionStrings = connectionStrings;
	DatabaseName = databaseName;
  }

  public string ConnectionStrings { get; init; }

  public string DatabaseName { get; init; }
}
