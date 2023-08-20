// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     DatabaseCollection.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : IssueTracker
// Project Name :  BlogService.UI.Tests.Integration
// =============================================

namespace IssueTracker.PlugIns;

[CollectionDefinition("Test Collection")]
public class DatabaseCollection : ICollectionFixture<IntegrationTestFactory>
{
	// This class has no code, and is never created. Its purpose is simply
	// to be the place to apply [CollectionDefinition] and all the
	// ICollectionFixture<> interfaces.
}