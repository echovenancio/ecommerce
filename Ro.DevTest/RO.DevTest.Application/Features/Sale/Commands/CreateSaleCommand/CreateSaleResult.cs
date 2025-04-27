namespace RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;

public class CreateSaleResult
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }

    public CreateSaleResult(Domain.Entities.Sale sale)
    {
        Id = sale.Id;
        UserId = sale.UserId;
        ProductId = sale.ProductId;
        Quantity = sale.Quantity;
        Total = sale.Total;
    }
}
