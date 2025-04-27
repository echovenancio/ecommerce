namespace RO.DevTest.Application.DTOs;

/// <summary>
/// Represents a product in the API
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Unique identifier for the product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the product
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the product
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Quantity of the product
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Date when the product was created
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Date when the product was last updated
    /// </summary>
    public DateTime ModifiedOn { get; set; }
}
