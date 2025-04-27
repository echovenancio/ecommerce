using MediatR;

namespace RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;

public class UpdateProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Domain.Entities.Product AssignTo()
    {
        return new Domain.Entities.Product
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            Quantity = Quantity,
        };
    }
}
