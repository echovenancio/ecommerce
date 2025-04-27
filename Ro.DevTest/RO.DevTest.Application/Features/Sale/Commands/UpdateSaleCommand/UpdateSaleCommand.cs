using MediatR;

namespace RO.DevTest.Application.Features.Sale.Commands.UpdateSaleCommand;

public class UpdateSaleCommand: IRequest<bool>
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public Domain.Entities.Sale AssignTo()
    {
        return new Domain.Entities.Sale
        {
            Id = Id,
            UserId = UserId,
            ProductId = ProductId,
            Quantity = Quantity,
            User = new Domain.Entities.User(),
            Product = new Domain.Entities.Product(),
        };
    }
}
