namespace RO.DevTest.Domain.Entities;

using RO.DevTest.Domain.Abstract;

/// <summary>
/// Represents a product in the API
/// </summary>
public class Product : BaseEntity
{
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

    public Product() : base() { }
}
