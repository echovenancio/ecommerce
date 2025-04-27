using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Common;

namespace RO.DevTest.Application.Features.User.Queries.GetUsersQuery;

/// <summary>
/// Query to get all users
/// </summary>
public class GetUsersQuery : IRequest<PagedResult<UserDto>>
{
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string OrderBy { get; set; } = "Name";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
