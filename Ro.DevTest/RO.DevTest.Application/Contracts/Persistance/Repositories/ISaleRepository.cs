namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

using RO.DevTest.Domain.Entities;
using RO.DevTest.Application.DTOs;

/// <summary>
/// Interface for the sale repository
/// </summary>
public interface ISaleRepository : IBaseRepository<Sale> {

    /// <summary>
    /// Get sales by date interval
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of sales</returns>
    Task<SalesReportDto?> GetSalesReportByDateIntervalAsync(DateTime startDate, DateTime endDate, Guid productId);
}
