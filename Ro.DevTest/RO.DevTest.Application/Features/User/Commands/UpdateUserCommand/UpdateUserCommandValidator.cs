using FluentValidation;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>{
    public UpdateUserCommandValidator() {
        RuleFor(cpau => cpau.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo nome de usuário precisa ser preenchido");

        RuleFor(cpau => cpau.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo e-mail precisa ser preenchido");

        RuleFor(cpau => cpau.Email)
            .EmailAddress()
            .WithMessage("O campo e-mail precisa ser um e-mail válido");
    }
}
