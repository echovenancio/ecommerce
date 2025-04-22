namespace RO.DevTest.Domain.Entities;

using RO.DevTest.Domain.Abstract;

/// <summary>
/// Represents a sale in the API
/// /// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// The quantity of the product sold
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The total price of the sale
    /// </summary>
    public decimal Total => Quantity * Product.Price;

    /// <summary>
    /// Id of the product sold 
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The product sold
    /// </summary>
    public required Product Product { get; set; }

    /// <summary>
    /// Id of the user who made the sale
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// The user who made the sale
    /// </summary>
    public required User User { get; set; }

    public Sale() : base() { }
}
