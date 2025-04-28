using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.User.Commands.CreateUserCommand;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Exception;
using RO.DevTest.Application.Features.User.Queries.GetSingleUserQuery;
using RO.DevTest.Application.Features.User.Queries.GetUsersQuery;
using RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;
using RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;
using RO.DevTest.Application.Common;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/user")]
[OpenApiTags("Users")]
public class UsersController(IMediator mediator) : Controller {
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request) {
        try {
            CreateUserResult response = await _mediator.Send(request);
            return Created(HttpContext.Request.GetDisplayUrl(), response);
        } catch (BadRequestException ex) {
            return BadRequest(ex.Errors);
        } catch (InternalServerErrorException ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById([FromRoute] string id) {
        try {
            var query = new GetSingleUserQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null) {
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
    [ProducesResponseType(typeof(PagedResult<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query) {
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
    public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserCommand command) {
        try {
            if (id != command.Id) {
                return BadRequest("Id in the URL and body do not match.");
            }
            var result = await _mediator.Send(command);
            if (!result) {
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
    public async Task<IActionResult> DeleteUser([FromRoute] string id) {
        try {
            var command = new DeleteUserCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result) {
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
