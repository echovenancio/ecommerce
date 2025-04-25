using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;

class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = _productRepository.Get(x => x.Id == request.Id);

        if (product == null)
            throw new BadRequestException("Produto não encontrado.");

        await _productRepository.DeleteAsync(product, cancellationToken);

        return true;
    }
}
