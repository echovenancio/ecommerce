using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Common;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Queries.GetSingleUserQuery;

public class GetSingleUserQueryHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<GetSingleUserQuery, UserDto>
{
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

    public async Task<UserDto> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityAbstractor.FindUserByIdAsync(request.Id);
        if (user == null)
        {
            throw new BadRequestException("Usuário não encontrado.");
        }

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}
