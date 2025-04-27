using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.DTOs;

/// <summary>
/// Represents a user in the API
/// </summary>
public class UserDto
{
    /// <summary>
    /// Unique identifier for the user
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Name of the user
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email of the user
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Username of the user
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Role of the user
    /// </summary>
    public UserRoles Role { get; set; }
}
