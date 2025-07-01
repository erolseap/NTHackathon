using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NTHackathon.Application.CQRS.Commands;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.WebApi.DTOs;

namespace NTHackathon.WebApi.Controllers;

[ApiController]
[Route("reservations/{id:int}")]
public class ReservationController : EntityControllerBase<Reservation>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ReservationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("", Name = "Get a specific reservation")]
    [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
    public IActionResult GetAsync(CancellationToken cancellationToken = default)
    {
        return Ok(_mapper.Map<ReservationDto>(Entity));
    }
    
    [HttpPatch("", Name = "Update a specific reservation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PatchAsync(
        [FromBody] ReservationControllerPatchDto data,
        CancellationToken cancellationToken = default)
    {
        return Ok(_mapper.Map<ReservationDto>(Entity));
    }

    [HttpDelete("", Name = "Delete a specific reservation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken = default)
    {
        var command = new RemoveReservationCommand()
        {
            Id =  Entity.Id
        };
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }
}