using MediatR;

namespace RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;

public class CreateSaleCommand: IRequest<CreateSaleResult>
{
    public string UserId { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public CreateSaleCommand(string userId, Guid productId, int quantity)
    {
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
    }

    public Domain.Entities.Sale AssignTo()
    {
        return new Domain.Entities.Sale
        {
            UserId = UserId,
            ProductId = ProductId,
            User = new Domain.Entities.User(),
            Product = new Domain.Entities.Product(),
        };
    }
}
