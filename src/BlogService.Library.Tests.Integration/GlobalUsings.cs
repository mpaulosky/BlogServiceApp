// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : GlobalUsings.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library.Tests.Integration
// =============================================

global using System.Diagnostics.CodeAnalysis;
global using System.Net;
global using BlogService.Library.Contracts;
global using BlogService.Library.DataAccess;
global using FluentAssertions;
global using BlogService.Library.FakerCreators;
global using BlogService.Library.Fixtures;
global using BlogService.Library.Models;
global using BlogService.Library.Services;
global using BlogService.UI;
global using JetBrains.Annotations;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.AspNetCore.TestHost;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using MongoDB.Bson;
global using MongoDB.Driver;
global using Testcontainers.MongoDb;
global using Xunit;
