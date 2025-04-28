using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Product.Queries.GetSingleProductQuery;

public class GetSingleProductQueryHandler(IProductRepository productRepository) : IRequestHandler<GetSingleProductQuery, ProductDto>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ProductDto> Handle(GetSingleProductQuery request, CancellationToken cancellationToken)
    {
        var product = _productRepository.Get(x => x.Id == request.Id);

        if (product == null)
            return null!;

        return new ProductDto{
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            CreatedOn = product.CreatedOn,
            ModifiedOn = product.ModifiedOn
        };
    }
}
