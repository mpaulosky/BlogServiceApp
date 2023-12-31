﻿// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     PriorityOrderer.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI.Tests.Playwright
// =============================================

namespace BlogService.UI.Priority;

public class PriorityOrderer : ITestCaseOrderer
{
	private readonly IMessageSink messageSink;

	public PriorityOrderer(IMessageSink messageSink)
	{
		this.messageSink = messageSink;
	}

	public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
		IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
	{
		string assemblyName = typeof(TestPriorityAttribute).AssemblyQualifiedName!;
		var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
		foreach (TTestCase testCase in testCases)
		{
			int priority = testCase.TestMethod.Method
				.GetCustomAttributes(assemblyName)
				.FirstOrDefault()
				?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? 0;

			GetOrCreate(sortedMethods, priority).Add(testCase);
		}

		var orderedCases = sortedMethods.Keys.SelectMany(
			priority => sortedMethods[priority].OrderBy(
				testCase => testCase.TestMethod.Method.Name)).ToList();

		var message = new DiagnosticMessage("Ordered {0} test cases", orderedCases.Count);
		messageSink.OnMessage(message);

		return orderedCases;

		//foreach (TTestCase testCase in
		//    sortedMethods.Keys.SelectMany(
		//        priority => sortedMethods[priority].OrderBy(
		//            testCase => testCase.TestMethod.Method.Name)))
		//{
		//    yield return testCase;
		//}
	}

	private static TValue GetOrCreate<TKey, TValue>(
		IDictionary<TKey, TValue> dictionary, TKey key)
		where TKey : struct
		where TValue : new() =>
		dictionary.TryGetValue(key, out TValue? result)
			? result
			: (dictionary[key] = new TValue());
}
