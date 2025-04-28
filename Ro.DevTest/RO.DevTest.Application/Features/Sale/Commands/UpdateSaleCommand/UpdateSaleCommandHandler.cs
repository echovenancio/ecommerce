using MediatR;

using RO.DevTest.Domain.Exception;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Contracts.Infrastructure;

namespace RO.DevTest.Application.Features.Sale.Commands.UpdateSaleCommand;

public class UpdateSaleCommandHandler(ISaleRepository saleRepository, IProductRepository productRepository, IIdentityAbstractor identityAbstractor) : IRequestHandler<UpdateSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository = saleRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

    public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult);
        }

        var sale = _saleRepository.Get(x => x.Id == request.Id);

        if (sale == null)
        {
            throw new BadRequestException("Venda não encontrada.");
        }

        var product = _productRepository.Get(x => x.Id == request.ProductId);
        if (product == null)
        {
            throw new BadRequestException("Produto não encontrado.");
        }

        var user = await _identityAbstractor.FindUserByIdAsync(request.UserId);
        if (user == null)
        {
            throw new BadRequestException("Usuário não encontrado.");
        }

        var requestSale = request.AssignTo();

        sale.User = user;
        sale.Product = product;
        sale.Quantity = requestSale.Quantity;
        sale.Total = product.Price * requestSale.Quantity;
        sale.ModifiedOn = DateTime.UtcNow;

        await _saleRepository.UpdateAsync(sale);

        return true;
    }
}
