// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     NotAuthorized.razor.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogServiceApp
// Project Name :  BlogService.UI
// =============================================

namespace BlogService.UI.Components;

public partial class NotAuthorized
{
  /// <summary>
  ///   Closes the page method.
  /// </summary>
  private void ClosePage() { NavManager.NavigateTo("/"); }
}
