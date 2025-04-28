using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Common;
using RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;
using RO.DevTest.Application.Features.Sale.Commands.UpdateSaleCommand;
using RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand;
using RO.DevTest.Application.Features.Sale.Queries.GetSingleSaleQuery;
using RO.DevTest.Application.Features.Sale.Queries.GetSalesQuery;
using RO.DevTest.Application.Features.Sale.Queries.GetSalesReportQuery;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/sale")]
[OpenApiTags("Sales")]
public class SaleController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request)
    {
        CreateSaleResult response = await _mediator.Send(request);
        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SaleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById([FromRoute] Guid id)
    {
        var query = new GetSingleSaleQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<SaleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSales([FromQuery] GetSalesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("report")]
    [ProducesResponseType(typeof(SalesReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSalesReport([FromQuery] GetSalesReportQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleCommand command)
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
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id)
    {
        var command = new DeleteSaleCommand { Id = id };
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
