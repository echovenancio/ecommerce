using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSalesReportQuery;

public class GetSalesReportQueryHandler(ISaleRepository saleRepository) : IRequestHandler<GetSalesReportQuery, SalesReportDto>
{
    private readonly ISaleRepository _saleRepository = saleRepository;

    public async Task<SalesReportDto> Handle(GetSalesReportQuery request, CancellationToken cancellationToken)
    {

        var salesReport = await _saleRepository.GetSalesReportByDateIntervalAsync(
            request.StartDate,
            request.EndDate,
            request.ProductId
                );

        if (salesReport == null)
            throw new BadRequestException("Nenhuma venda encontrada.");

        return salesReport;
    }
}
