using MediatR;
using RO.DevTest.Domain.Exception;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Contracts.Infrastructure;

namespace RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;

public class CreateSaleCommandHandler(ISaleRepository saleRepository, IProductRepository productRepository, IIdentityAbstractor identityAbstractor) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository = saleRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Log the errors
            Console.WriteLine("Validation errors:");
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
            }
            throw new BadRequestException(validationResult);
        }

        var sale = request.AssignTo();
        var product = _productRepository.Get(x => x.Id == sale.ProductId);

        if (product == null)
            throw new BadRequestException("Produto não encontrado.");

        var user = await _identityAbstractor.FindUserByIdAsync(sale.UserId);

        if (user == null)
            throw new BadRequestException("Usuário não encontrado."); 

        sale.User = user;
        sale.Product = product;
        sale.Total = product.Price * sale.Quantity;

        var result = await _saleRepository.CreateAsync(sale, cancellationToken);

        if (result == null)
            throw new BadRequestException("Venda não encontrada.");

        return new CreateSaleResult(result);
    }
}
