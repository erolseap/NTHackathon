using AutoMapper;
using MediatR;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Queries;

public class FindRoomByPriceQuery : IRequest<RoomDto?>
{
    public decimal? MinimumPricePerNight { get; set; }
    public decimal? MaximumPricePerNight { get; set; }
}

public class FindRoomByPriceQueryHandler : IRequestHandler<FindRoomByPriceQuery, RoomDto?>
{
    private readonly IReadOnlyRepositoryAsync<Room> _repository;
    private readonly IMapper _mapper;

    public FindRoomByPriceQueryHandler(IReadOnlyRepositoryAsync<Room> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RoomDto?> Handle(FindRoomByPriceQuery request, CancellationToken cancellationToken)
    {
        var specification = new GetRoomByPriceSpecification(
            request.MinimumPricePerNight,
            request.MaximumPricePerNight
        );

        var room = await _repository.FirstOrDefaultAsync(specification, cancellationToken: cancellationToken);
        return _mapper.Map<RoomDto?>(room);
    }
}
