// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : IDatabaseSettings.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library
// =============================================

namespace BlogService.Library.Contracts;

public interface IDatabaseSettings
{
	string ConnectionStrings { get; init; }

	string DatabaseName { get; init; }
}
