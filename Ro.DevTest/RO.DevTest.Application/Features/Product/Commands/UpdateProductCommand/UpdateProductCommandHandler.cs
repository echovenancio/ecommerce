using MediatR;
using FluentValidation.Results;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;

public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        UpdateProductCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            // Log the errors
            Console.WriteLine("Validation errors:");
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
            }
            throw new BadRequestException(validationResult);
        }

        var requestProduct = request.AssignTo();

        var product = _productRepository.Get(x => x.Id == requestProduct.Id);

        if (product == null)
            throw new BadRequestException("Produto não encontrado.");

        product.Name = requestProduct.Name;
        product.Description = requestProduct.Description;
        product.Price = requestProduct.Price;
        product.Quantity = requestProduct.Quantity;
        product.ModifiedOn = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product);

        return true; 
    }
}
