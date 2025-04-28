using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Common;
using RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;
using RO.DevTest.Application.Features.Product.Queries.GetSingleProductQuery;
using RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/product")]
[OpenApiTags("Products")]
public class ProductController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
    {
        CreateProductResult response = await _mediator.Send(request);
        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id)
    {
        var query = new GetSingleProductQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Id in the URL and body do not match.");
        }
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        var command = new DeleteProductCommand { Id = id };
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
