using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSingleSaleQuery;

public class GetSingleSaleQueryHandler(ISaleRepository saleRepository) : IRequestHandler<GetSingleSaleQuery, SaleDto>
{
    private readonly ISaleRepository _saleRepository = saleRepository;

    public async Task<SaleDto> Handle(GetSingleSaleQuery request, CancellationToken cancellationToken)
    {
        var sale = _saleRepository.Get(x => x.Id == request.Id);

        if (sale == null)
            return null!; 

        return new SaleDto
        {
            Id = sale.Id,
            UserId = sale.UserId,
            ProductId = sale.ProductId,
            SaleDate = sale.CreatedOn,
            Quantity = sale.Quantity,
            TotalPrice = sale.Product.Price * sale.Quantity,
        };
    }
}
