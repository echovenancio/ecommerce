using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

/// <summary>
/// Command handler for the update of <see cref="Domain.Entities.User"/>
/// </summary>
public class UpdateUserCommandHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<UpdateUserCommand, bool> {
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
        UpdateUserCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid) {
            // Log the errors
            Console.WriteLine("Validation errors:");
            foreach (var error in validationResult.Errors) {
                Console.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
            }
            throw new BadRequestException(validationResult);
        }

        var user = await _identityAbstractor.FindUserByIdAsync(request.Id);

        if (user == null)
            throw new BadRequestException("User not found.");

        Domain.Entities.User updatedUser = request.AssignTo();

        user.UserName = updatedUser.UserName;
        user.Email = updatedUser.Email;
        user.Name = updatedUser.Name;

        IdentityResult userCreationResult = await _identityAbstractor.UpdateUserAsync(user);


        if(!userCreationResult.Succeeded) {
            var errors = string.Join("; ", userCreationResult.Errors.Select(e => $"{e.Code}: {e.Description}"));
            Console.WriteLine(errors);
            throw new BadRequestException(userCreationResult);
        }

        IdentityResult userRoleResult = await _identityAbstractor.AddToRoleAsync(user, request.Role);

        if(!userRoleResult.Succeeded) {
            throw new BadRequestException(userRoleResult);
        }

        return true;
    }
}
