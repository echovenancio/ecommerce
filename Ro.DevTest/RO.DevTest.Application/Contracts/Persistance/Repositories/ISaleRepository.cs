namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

using RO.DevTest.Domain.Entities;

/// <summary>
/// Interface for the sale repository
/// </summary>
public interface ISaleRepository : IBaseRepository<Sale> {
    /// <summary>
    /// Get all sales for a user
    /// </summary>
    /// <param name="userId">Id of the user</param>
    /// <returns>List of sales</returns>
    Task<IReadOnlyList<Sale>> GetSalesByUserIdAsync(string userId);

    /// <summary>
    /// Get all sales for a product
    /// </summary>
    /// <param name="productId">Id of the product</param>
    /// <returns>List of sales</returns>
    Task<IReadOnlyList<Sale>> GetSalesByProductIdAsync(string productId);

    /// <summary>
    /// Get all sales total
    /// </summary>
    /// <returns>Total sales</returns>
    Task<decimal> GetTotalSalesAsync();

    /// <summary>
    /// Get sales count
    /// </summary>
    /// <returns>Count of sales</returns>
    Task<int> GetSalesCountAsync();

    /// <summary>
    /// Get sales by date interval
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of sales</returns>
    Task<IReadOnlyList<Sale>> GetSalesByDateIntervalAsync(DateTime startDate, DateTime endDate);
}
