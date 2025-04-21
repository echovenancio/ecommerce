using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Persistence.Repositories;

public class SaleRepository(DefaultContext context) : BaseRepository<Sale>(context), ISaleRepository {

    public async Task<SalesReportDto?> GetSalesReportByDateIntervalAsync(
        DateTime startDate,
        DateTime endDate,
        Guid productId
    ) {
        var salesQuery = context.Sales
            .Where(s => s.CreatedOn >= startDate &&
                        s.CreatedOn <= endDate &&
                        s.ProductId == productId)
            .Select(s => new {
                s.Total,
                s.Quantity,
                ProductName = s.Product.Name
            });

        var sales = await salesQuery.ToListAsync();

        if (sales.Count == 0) return null;

        var totalSales = sales.Sum(s => s.Total);
        var totalQuantity = sales.Sum(s => s.Quantity);
        var productName = sales.First().ProductName;

        return new SalesReportDto {
            ProductId = productId,
            ProductName = productName,
            TotalSales = totalSales,
            TotalUnitsSold = totalQuantity
        };     
    }

}
