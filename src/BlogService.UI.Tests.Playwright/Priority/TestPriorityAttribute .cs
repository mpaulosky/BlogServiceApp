﻿namespace BlogService.UI.Tests.Playwright.Priority;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TestPriorityAttribute : Attribute
{
	public int Priority { get; private set; }

	public TestPriorityAttribute(int priority) => Priority = priority;
}
