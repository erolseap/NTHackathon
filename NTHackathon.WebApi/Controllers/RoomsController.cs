using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NTHackathon.Application.CQRS.Commands;
using NTHackathon.Application.CQRS.Queries;
using NTHackathon.Domain.DTOs;
using NTHackathon.WebApi.DTOs;
using NTHackathon.WebApi.Interfaces;

namespace NTHackathon.WebApi.Controllers;

[ApiController]
[Route("rooms")]
public class RoomsController : ManagedControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    public RoomsController(IMediator mediator, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }




    [HttpGet("by-customer/{customerId:int}")]
    [ProducesResponseType(typeof(IEnumerable<RoomDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoomsByCustomerId(int customerId)
    {
        var rooms = await _mediator.Send(new FindRoomsByCustomerIdQuery(customerId));


        return Ok(rooms);
    }





    [HttpGet("", Name = "Get all rooms")]
    [ProducesResponseType(typeof(IEnumerable<RoomDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetAllRoomsQuery(), cancellationToken));
    }

    [HttpPost("", Name = "Create a new room")]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromForm] RoomsControllerCreateDto data,
        CancellationToken cancellationToken = default)
    {
        var imageUrl = await _cloudinaryService.FileCreateAsync(data.Image!);
        var id = await _mediator.Send(new CreateRoomCommand()
        {
            IsReserved = data.IsReserved!.Value,
            Number = data.Number!.Value,
            PricePerNight = data.PricePerNight!.Value,
            Type = data.Type!.Value,
            ImageUrl = imageUrl
        }, cancellationToken);
        var room = await _mediator.Send(new FindRoomByIdQuery() { Id = id }, cancellationToken);
        return Created((Uri?)null, room);
    }
}
