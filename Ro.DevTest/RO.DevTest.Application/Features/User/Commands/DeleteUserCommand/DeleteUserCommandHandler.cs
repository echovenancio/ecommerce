using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;

public class DeleteUserCommandHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityAbstractor.FindUserByIdAsync(request.Id);
        if (user == null)
        {
            throw new BadRequestException("User not found");
        }

        await _identityAbstractor.DeleteUser(user);
        return true;
    }
}
