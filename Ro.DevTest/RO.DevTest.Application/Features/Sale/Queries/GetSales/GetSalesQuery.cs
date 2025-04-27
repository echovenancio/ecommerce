using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Common;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSalesQuery;

/// <summary>
/// Query to get a list of sales
/// </summary>
public class GetSalesQuery : IRequest<PagedResult<SaleDto>>
{
    public string? CustomerName { get; set; } = string.Empty;
    public string? ProductName { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = "Date";
}
