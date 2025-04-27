using MediatR;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;

public class DeleteUserCommand : IRequest<bool>
{
    public string Id { get; set; } = string.Empty;
}
