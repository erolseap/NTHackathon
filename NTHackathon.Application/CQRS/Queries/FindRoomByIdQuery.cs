using AutoMapper;
using MediatR;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Queries;

public class FindRoomByIdQuery : IRequest<RoomDto?>
{
    public int Id { get; set; }


    class FindRoomByIdQueryHandler : IRequestHandler<FindRoomByIdQuery, RoomDto?>
    {
        private readonly IReadOnlyRepositoryAsync<Room> _repository;
        private readonly IMapper _mapper;

        public FindRoomByIdQueryHandler(IReadOnlyRepositoryAsync<Room> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RoomDto?> Handle(FindRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await _repository.FirstOrDefaultAsync(new GetEntityByIdSpecification<Room>(request.Id));
            return _mapper.Map<RoomDto?>(room);
        }
    }
}
