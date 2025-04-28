namespace RO.DevTest.Application.DTOs;

/// <summary>
/// Represents a sales report in the API
/// /// </summary>
public class SalesReportDto
{
    /// <summary>
    /// Unique identifier for the product
    /// /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Name of the product
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Total number of units sold
    /// </summary>
    public int TotalUnitsSold { get; set; }

    /// <summary>
    /// Total sales amount
    /// </summary>
    public decimal TotalSales { get; set; }

    /// <summary>
    /// Start date of the sale
    /// </summary>
    public DateTime StartSaleDate { get; set; }

    /// <summary>
    /// End date of the sale
    /// </summary>
    public DateTime EndSaleDate { get; set; }
}
