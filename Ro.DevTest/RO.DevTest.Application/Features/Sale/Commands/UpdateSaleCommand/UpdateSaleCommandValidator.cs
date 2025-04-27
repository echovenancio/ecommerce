using FluentValidation;

namespace RO.DevTest.Application.Features.Sale.Commands.UpdateSaleCommand;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id é necessário.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId é necessário.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId é necessário.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantidade precisa ser maior ou igual a 0.");
    }
}
