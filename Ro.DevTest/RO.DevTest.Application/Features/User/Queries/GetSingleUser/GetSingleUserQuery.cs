using MediatR;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Features.User.Queries.GetSingleUserQuery;

/// <summary>
/// Query to get a single user by ID
/// </summary>
public class GetSingleUserQuery : IRequest<UserDto>
{
    public string Id { get; set; } = string.Empty;
}
