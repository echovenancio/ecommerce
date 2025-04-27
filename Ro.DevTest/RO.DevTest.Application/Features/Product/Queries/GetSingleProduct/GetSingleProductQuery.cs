using MediatR;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Features.Product.Queries.GetSingleProductQuery;

public class GetSingleProductQuery : IRequest<ProductDto>
{
    public Guid Id { get; set; }
}
