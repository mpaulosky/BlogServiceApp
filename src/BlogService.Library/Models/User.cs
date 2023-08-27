// ============================================
//   Copyright (c) 2023. All rights reserved.
//   File Name     : User.cs
//   Company       : mpaulosky
//   Author        : Matthew Paulosky
//   Solution Name : BlogServiceApp
//   Project Name  : BlogService.Library
// =============================================

namespace BlogService.Library.Models;

/// <summary>
///   UserModel class
/// </summary>
[Serializable]
public class User
{
	/// <summary>
	///   Gets or sets the identifier.
	/// </summary>
	/// <value>
	///   The identifier.
	/// </value>
	[BsonId]
	[BsonElement("_id")]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; } = string.Empty;

	/// <summary>
	///   Gets or sets the object identifier.
	/// </summary>
	/// <value>
	///   The object identifier.
	/// </value>
	[BsonElement("object_identifier")]
	public string ObjectIdentifier { get; set; } = string.Empty;

	/// <summary>
	///   Gets or sets the first name.
	/// </summary>
	/// <value>
	///   The first name.
	/// </value>
	[BsonElement("first_name")]
	[BsonRepresentation(BsonType.String)]
	public string FirstName { get; set; } = string.Empty;

	/// <summary>
	///   Gets or sets the last name.
	/// </summary>
	/// <value>
	///   The last name.
	/// </value>
	[BsonElement("last_name")]
	[BsonRepresentation(BsonType.String)]
	public string LastName { get; set; } = string.Empty;

	/// <summary>
	///   Gets or sets the display name.
	/// </summary>
	/// <value>
	///   The display name.
	/// </value>
	[BsonElement("display_name")]
	[BsonRepresentation(BsonType.String)]
	public string DisplayName { get; set; } = string.Empty;

	/// <summary>
	///   Gets or sets the email address.
	/// </summary>
	/// <value>
	///   The email address.
	/// </value>
	[BsonElement("email_address")]
	[BsonRepresentation(BsonType.String)]
	public string EmailAddress { get; set; } = string.Empty;

	/// <summary>
	///   Gets or sets a value indicating whether this <see cref="User" /> is archived.
	/// </summary>
	/// <value>
	///   <c>true</c> if archived; otherwise, <c>false</c>.
	/// </value>
	[BsonElement("archived")]
	[BsonRepresentation(BsonType.Boolean)]
	public bool Archived { get; set; }

	/// <summary>
	///   Gets or sets who archived the record.
	/// </summary>
	/// <value>
	///   Who archived the record.
	/// </value>
	public BasicUser ArchivedBy { get; set; } = new();
}
