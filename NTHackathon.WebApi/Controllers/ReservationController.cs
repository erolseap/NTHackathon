using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NTHackathon.Application.CQRS.Commands;
using NTHackathon.Application.Repositories;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Services;
using NTHackathon.WebApi.DTOs;

namespace NTHackathon.WebApi.Controllers;

[ApiController]
[Route("reservations/{id:int}")]
public class ReservationController : EntityControllerBase<Reservation>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IPaymentService _paymentService;
    private readonly IUnitOfWork _unitOfWork;

    public ReservationController(IMediator mediator, IMapper mapper, IPaymentService paymentService, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _mapper = mapper;
        _paymentService = paymentService;
        _unitOfWork = unitOfWork;
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
        [FromForm] ReservationControllerPatchDto data,
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

    [HttpPost("payment", Name = "Get a url to pay")]
    [ProducesResponseType(typeof(ReservationControllerGetPaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [TrackedEntity<Reservation>]
    public async Task<IActionResult> GetPaymentAsync([FromBody] PaymentCreateDto data, CancellationToken cancellationToken = default)
    {
        if (Entity.IsPaid)
        {
            return NoContent();
        }
        var response = await _paymentService.CreateAsync(data);
        var order = response.Order;
        Entity.PaymentId = order.Id;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Ok(new ReservationControllerGetPaymentDto()
        {
            PaymentUrl = $"{order.HppUrl}?id={order.Id}&password={order.Password}"
        });
    }
    
    [HttpGet("payment", Name = "Notification about paid url")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CheckPaymentAsync(PaymentCheckDto dto)
    {
        var result = await _paymentService.CheckPaymentAsync(dto);
        if (!result)
        {
            return Forbid();
        }

        return Ok();
    }
}
