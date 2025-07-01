using MediatR;
using Microsoft.AspNetCore.Mvc;
using NTHackathon.Application.CQRS.Commands;
using NTHackathon.Application.CQRS.Queries;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.WebApi.DTOs;

namespace NTHackathon.WebApi.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ManagedControllerBase
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("", Name = "List all reservations")]
    [ProducesResponseType(typeof(IEnumerable<ReservationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetAllReservationsQuery(), cancellationToken));
    }

    [HttpPost("", Name = "Create a new reservation")]
    [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] ReservationsControllerCreateDto data,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateReservationCommand()
        {
            CustomerId = data.CustomerId!.Value,
            RoomId = data.RoomId!.Value,
            CheckInDate = data.CheckInDate!.Value,
            CheckOutDate = data.CheckOutDate!.Value,
        };
        var id = await _mediator.Send(command, cancellationToken);
        var reservation = _mediator.Send(new GetEntityByIdSpecification<Reservation>(id), cancellationToken);
        return Created((Uri?)null, reservation);
    }
}
