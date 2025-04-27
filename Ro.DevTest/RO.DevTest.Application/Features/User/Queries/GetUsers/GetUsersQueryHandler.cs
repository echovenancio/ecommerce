using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Common;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Linq.Expressions;

namespace RO.DevTest.Application.Features.User.Queries.GetUsersQuery;
public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<PagedResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {

        Expression<Func<Domain.Entities.User, bool>> predicate = p =>
            (string.IsNullOrEmpty(request.Name) || p.Name.Contains(request.Name)) &&
            (string.IsNullOrEmpty(request.Email) || p.Email.Contains(request.Email)) &&
            (string.IsNullOrEmpty(request.UserName) || p.UserName.Contains(request.UserName));

        var result = await _userRepository.GetPagedAsync(
                predicate,
                q => request.OrderBy!.ToLower() switch
                {
                    "name" => q.OrderBy(x => x.Name),
                    "email" => q.OrderBy(x => x.Email),
                    "username" => q.OrderBy(x => x.UserName),
                    _ => q.OrderBy(x => x.Name)
                },
                request.PageNumber,
                request.PageSize);

        return new PagedResult<UserDto>(
            result.TotalCount,
            result.PageSize,
            result.PageNumber,
            result.Items.Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                Email = x.Email
            }).ToList());
    }
}
