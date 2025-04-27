namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public record CreateProductResult {
   public Guid Id { get; set; }  
   public string Name { get; set; } = string.Empty;
   public string Description { get; set; } = string.Empty;
   public decimal Price { get; set; }
   public int Quantity { get; set; }

   public CreateProductResult(Domain.Entities.Product product) {
      Id = product.Id;
      Name = product.Name;
      Description = product.Description;
      Price = product.Price;
      Quantity = product.Quantity;
   }

}
