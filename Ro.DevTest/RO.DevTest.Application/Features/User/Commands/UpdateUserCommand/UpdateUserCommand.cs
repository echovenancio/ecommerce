using MediatR;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommand : IRequest<bool> {
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRoles Role { get; set; }

    public Domain.Entities.User AssignTo() {
        return new Domain.Entities.User {
            Id = Id,
            UserName = UserName,
            Email = Email,
            Name = Name,
        };
    }
}
