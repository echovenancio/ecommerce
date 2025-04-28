using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Common;
using RO.DevTest.Domain.Exception;
using RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;
using RO.DevTest.Application.Features.Sale.Commands.UpdateSaleCommand;
using RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand;
using RO.DevTest.Application.Features.Sale.Queries.GetSingleSaleQuery;
using RO.DevTest.Application.Features.Sale.Queries.GetSalesQuery;
using RO.DevTest.Application.Features.Sale.Queries.GetSalesReportQuery;
using Microsoft.AspNetCore.Authorization;
using RO.DevTest.WebApi.Authorization;

namespace RO.DevTest.WebApi.Controllers;

[Authorize(Policy = IdentityPolicies.AdminPolicy)]
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
        try {
            CreateSaleResult response = await _mediator.Send(request);
            return Created(HttpContext.Request.GetDisplayUrl(), response);
        } catch (BadRequestException ex) {
            return BadRequest(ex.Errors);
        } catch (InternalServerErrorException ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SaleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById([FromRoute] Guid id)
    {
        try {
            var query = new GetSingleSaleQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        } catch (BadRequestException ex) {
            return BadRequest(ex.Errors);
        } catch (InternalServerErrorException ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<SaleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSales([FromQuery] GetSalesQuery query)
    {
        try {
            var result = await _mediator.Send(query);
            return Ok(result);
        } catch (BadRequestException ex) {
            return BadRequest(ex.Errors);
        } catch (InternalServerErrorException ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("report")]
    [ProducesResponseType(typeof(SalesReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSalesReport([FromQuery] GetSalesReportQuery query)
    {
        try {
            var result = await _mediator.Send(query);
            return Ok(result);
        } catch (BadRequestException ex) {
            return BadRequest(ex.Errors);
        } catch (InternalServerErrorException ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleCommand command)
    {
        try {
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
        } catch (BadRequestException ex) {
            return BadRequest(ex.Errors);
        } catch (InternalServerErrorException ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id)
    {
        try {
            var command = new DeleteSaleCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        } catch (BadRequestException ex) {
            return BadRequest(ex.Errors);
        } catch (InternalServerErrorException ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
