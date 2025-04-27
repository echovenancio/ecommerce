namespace RO.DevTest.Application.DTOs;

/// <summary>
/// Represents a sale in the API
/// /// </summary>
public class SaleDto
{
    /// <summary>
    /// Unique identifier for the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Unique identifier for the product
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Unique identifier for the user
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Name of the user
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Price of the product
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Date of the sale
    /// </summary>
    public DateTime SaleDate { get; set; }
}
