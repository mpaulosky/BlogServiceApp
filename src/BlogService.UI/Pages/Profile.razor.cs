// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     Profile.razor.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI
// =============================================

namespace BlogService.UI.Pages;

/// <summary>
///   Profile page class
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
[UsedImplicitly]
public partial class Profile
{
  private User _loggedInUser;

  /// <summary>
  ///   OnInitializedAsync event
  /// </summary>
  protected override async Task OnInitializedAsync()
  {
	_loggedInUser = await AuthProvider.GetUserFromAuth(UserService);
  }

  /// <summary>
  ///   ClosePage method
  /// </summary>
  private void ClosePage()
  {
	NavManager.NavigateTo("/");
  }
}
