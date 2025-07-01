using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NTHackathon.Application.CQRS.Commands;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.WebApi.DTOs;

namespace NTHackathon.WebApi.Controllers;

[ApiController]
[Route("services/{id:int}")]
public class ServiceController : EntityControllerBase<ServiceEntity>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ServiceController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("", Name = "Get a specific service")]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status200OK)]
    public IActionResult GetAsync(CancellationToken cancellationToken = default)
    {
        return Ok(_mapper.Map<ServiceDto>(Entity));
    }
    
    [HttpPatch("", Name = "Update a specific service")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult PatchAsync(
        [FromBody] ServiceControllerPatchDto data,
        CancellationToken cancellationToken = default)
    {
        return Ok(_mapper.Map<ServiceDto>(Entity));
    }

    [HttpDelete("", Name = "Delete a specific service")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken = default)
    {
        var command = new RemoveServiceCommand()
        {
            Id = Entity.Id
        };
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }
}