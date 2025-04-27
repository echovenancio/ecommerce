using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Common;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;

public class GetProductsQuery : IRequest<PagedResult<ProductDto>>
{
    public string? Name { get; set; } = string.Empty;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = "Name";
}
