// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     TestFixtures.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  TestingSupport.Library
// =============================================

using BlogService.Library.Contracts;
using BlogService.Library.DataAccess;

namespace TestingSupport.Library.Fixtures;

[ExcludeFromCodeCoverage]
public static class TestFixtures
{
	public static Mock<IAsyncCursor<TEntity>> GetMockCursor<TEntity>(IEnumerable<TEntity> list) where TEntity : class
	{
		Mock<IAsyncCursor<TEntity>> cursor = new();
		cursor.Setup(_ => _.Current).Returns(list);
		cursor
			.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
			.Returns(true)
			.Returns(false);
		cursor
			.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(true))
			.Returns(Task.FromResult(false));
		return cursor;
	}

	public static Mock<IMongoCollection<TEntity>> GetMockCollection<TEntity>(Mock<IAsyncCursor<TEntity>> cursor)
		where TEntity : class
	{
		Mock<IMongoCollection<TEntity>> collection = new() { Name = CollectionNames.GetCollectionName(nameof(TEntity)) };

		collection.Setup(op =>
				op.FindAsync
				(
					It.IsAny<FilterDefinition<TEntity>>(),
					It.IsAny<FindOptions<TEntity, TEntity>>(),
					It.IsAny<CancellationToken>()
				))
			.ReturnsAsync(cursor.Object);

		collection.Setup(op =>
			op.InsertOneAsync
			(
				It.IsAny<TEntity>(),
				It.IsAny<InsertOneOptions>(),
				It.IsAny<CancellationToken>()
			)).Returns(Task.CompletedTask);

		return collection;
	}

	public static Mock<IMongoDbContextFactory> GetMockContext()
	{
		Mock<IMongoClient> mockClient = new();
		Mock<IMongoDatabase> mockDatabase = new();
		Mock<IMongoDbContextFactory> context = new();
		Mock<IClientSessionHandle> mockSession = new();
		context.Setup(op => op.Client).Returns((Delegate)mockClient.Object);
		context.Setup(op => op.DbName).Returns((Delegate)mockDatabase.Object);
		context.Setup(op =>
				op.Client.StartSessionAsync(
					It.IsAny<ClientSessionOptions>(),
					It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockSession.Object));

		return context;
	}

	public static Mock<IMongoDbContextFactory> GetMockContextWithOutDataBase()
	{
		Mock<IMongoClient> mockClient = new();
		Mock<IMongoDbContextFactory> context = new();
		Mock<IClientSessionHandle> mockSession = new();
		context.Setup(op => op.Client).Returns((Delegate)mockClient.Object);
		context.Setup(op =>
				op.Client.StartSessionAsync(
					It.IsAny<ClientSessionOptions>(),
					It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(mockSession.Object));

		return context;
	}
}
