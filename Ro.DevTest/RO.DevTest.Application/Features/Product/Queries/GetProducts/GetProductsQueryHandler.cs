using MediatR;
using RO.DevTest.Application.Common;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Linq.Expressions;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;

public class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<PagedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Product, bool>> predicate = p =>
            (string.IsNullOrEmpty(request.Name) || p.Name.Contains(request.Name)) &&
            (!request.MinPrice.HasValue || p.Price >= request.MinPrice) &&
            (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice);
        var result = await _productRepository.GetPagedAsync(
                predicate,
                q => request.SortBy!.ToLower() switch
                {
                    "name" => q.OrderBy(x => x.Name),
                    "price" => q.OrderBy(x => x.Price),
                    "quantity" => q.OrderBy(x => x.Quantity),
                    _ => q.OrderBy(x => x.Name)
                },
                request.PageNumber,
                request.PageSize
        );

        return new PagedResult<ProductDto>(
            result.TotalCount,
            result.PageSize,
            result.PageNumber,
            result.Items.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Quantity = x.Quantity,
                CreatedOn = x.CreatedOn,
                ModifiedOn = x.ModifiedOn
            }).ToList());
    }
}

