// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     AuthenticationStateProviderHelpers.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI
// =============================================

namespace BlogService.UI.Helpers;

public static class AuthenticationStateProviderHelpers
{
	public static async Task<User> GetUserFromAuth(
		this AuthenticationStateProvider provider,
		IUserService userService)
	{
		var authState = await provider.GetAuthenticationStateAsync();
		var objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;
		return await userService.GetByAuthIdAsync(objectId);
	}
}
