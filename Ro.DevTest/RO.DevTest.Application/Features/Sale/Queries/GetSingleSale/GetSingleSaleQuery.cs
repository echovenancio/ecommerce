using MediatR;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSingleSaleQuery;

public class GetSingleSaleQuery : IRequest<SaleDto>
{
    public Guid Id { get; set; }
}
