using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NTHackathon.Application.CQRS.Commands;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.WebApi.DTOs;

namespace NTHackathon.WebApi.Controllers;

[ApiController]
[Route("rooms/{id:int}")]
public class RoomController : EntityControllerBase<Room>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RoomController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("", Name = "Get a specific room")]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
    public IActionResult Index()
    {
        return Ok(_mapper.Map<RoomDto>(Entity));
    }

    [HttpPatch("", Name = "Update a specific room")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] RoomsControllerUpdateDto data,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateRoomCommand()
        {
            Id = Entity.Id,
            Type = data.Type,
            IsReserved = data.IsReserved,
            PricePerNight = data.PricePerNight,
        };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Error);
        }

        return Ok();
    }

    [HttpDelete("", Name = "Delete a specific room")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.Send(
            new RemoveRoomCommand()
            {
                Id = Entity.Id
            }, cancellationToken);

        return Ok();
    }
}
