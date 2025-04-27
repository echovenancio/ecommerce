using MediatR;

namespace RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand;

public class DeleteSaleCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
