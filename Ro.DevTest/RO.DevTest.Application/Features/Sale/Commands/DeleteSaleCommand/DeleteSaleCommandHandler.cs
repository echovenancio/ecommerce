using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand;

public class DeleteSaleCommandHandler(ISaleRepository saleRepository) : IRequestHandler<DeleteSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository = saleRepository;

    public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = _saleRepository.Get(x => x.Id == request.Id);
        if (sale == null)
        {
            throw new BadRequestException("Compra não encontrada.");
        }

        await _saleRepository.DeleteAsync(sale);
        return true;
    }
}
