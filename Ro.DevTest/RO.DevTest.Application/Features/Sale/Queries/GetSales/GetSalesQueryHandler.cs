using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Common;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Linq.Expressions;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSalesQuery;

public class GetSalesQueryHandler(ISaleRepository saleRepository) : IRequestHandler<GetSalesQuery, PagedResult<SaleDto>>
{
    private readonly ISaleRepository _saleRepository = saleRepository;
    public async Task<PagedResult<SaleDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Sale, bool>> predicate = p =>
            (string.IsNullOrEmpty(request.CustomerName) || p.User.Name.Contains(request.CustomerName)) &&
            (string.IsNullOrEmpty(request.ProductName) || p.Product.Name.Contains(request.ProductName));
        var result = await _saleRepository.GetPagedAsync(
                predicate,
                q => request.SortBy!.ToLower() switch
                {
                    "customername" => q.OrderBy(x => x.User.Name),
                    "productname" => q.OrderBy(x => x.Product.Name),
                    _ => q.OrderBy(x => x.CreatedOn)
                },
                request.PageNumber,
                request.PageSize);

        return new PagedResult<SaleDto>(
            result.TotalCount,
            result.PageSize,
            result.PageNumber,
            result.Items.Select(x => new SaleDto
            {
                Id = x.Id,
                UserId = x.UserId,
                ProductId = x.ProductId,
                SaleDate = x.CreatedOn,
                Quantity = x.Quantity,
                TotalPrice = x.Product.Price * x.Quantity,
            }).ToList());
    }
}
