using MediatR;
using Microsoft.AspNetCore.Mvc;
using NTHackathon.Application.CQRS.Commands;
using NTHackathon.Application.CQRS.Queries;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.WebApi.DTOs;

namespace NTHackathon.WebApi.Controllers;

[ApiController]
[Route("services")]
public class ServicesController : ManagedControllerBase
{
    private readonly IMediator _mediator;

    public ServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("", Name = "List all services")]
    [ProducesResponseType(typeof(IEnumerable<ReservationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetAllServicesQuery(), cancellationToken));
    }

    [HttpPost("", Name = "Create a new service")]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] ServicesControllerCreateDto data,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateServiceCommand()
        {
            Name = data.Name!,
            Price = data.Price!.Value,
            ReservationId = data.ReservationId!.Value,
        };
        var id = await _mediator.Send(command, cancellationToken);
        var service = await _mediator.Send(new FindServiceByIdQuery() { Id = id}, cancellationToken);
        return Created((Uri?)null, service);
    }
}
