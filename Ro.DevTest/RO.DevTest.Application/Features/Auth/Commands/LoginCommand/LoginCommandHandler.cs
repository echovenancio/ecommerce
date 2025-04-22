namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;

public class LoginCommandHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<LoginCommand, LoginResponse> {
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken) {
        var user = await _identityAbstractor.FindUserByEmailAsync(request.Username);

        if (user == null) {
            throw new Exception("User not found");
        }

        var result = await _identityAbstractor.PasswordSignInAsync(user, request.Password);

        if (result.IsNotAllowed) {
            throw new Exception("User not allowed");
        }

        var token = await _identityAbstractor.GenerateUserTokenAsync(user);

        if (token == null) {
            throw new Exception("Token generation failed");
        }

        return new LoginResponse {
            AccessToken = token,
            Roles = await _identityAbstractor.GetUserRolesAsync(user),
            ExpirationDate = DateTime.UtcNow.AddHours(3),
        };

    }
}
