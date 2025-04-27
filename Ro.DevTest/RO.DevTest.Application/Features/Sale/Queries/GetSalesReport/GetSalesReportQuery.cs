using MediatR;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSalesReportQuery;

public class GetSalesReportQuery : IRequest<SalesReportDto>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid ProductId { get; set; }
}
