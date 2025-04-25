using FluentValidation;

namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(cpau => cpau.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Prenchimento do nome é necessário.");

        RuleFor(cpau => cpau.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Descrição não pode ser vazia.");

        RuleFor(cpau => cpau.Price)
            .GreaterThan(0)
            .WithMessage("Preço deve ser maior que 0.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantidade deve ser maior ou igual a 0.");
    }
}
