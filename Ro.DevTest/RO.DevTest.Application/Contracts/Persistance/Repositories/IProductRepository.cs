namespace RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;

/// <summary>
/// Interface for the product repository
/// </summary>
public interface IProductRepository : IBaseRepository<Product> {
    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>List of products</returns>
    Task<IReadOnlyList<Product>> GetAllProductsAsync();

    /// <summary>
    /// Get a product by id
    /// </summary>
    /// <param name="id">Id of the product</param>
    /// <returns>Product</returns>
    Task<Product?> GetProductByIdAsync(string id);
}
