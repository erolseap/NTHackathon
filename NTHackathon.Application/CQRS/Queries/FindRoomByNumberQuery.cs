using AutoMapper;
using MediatR;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Queries;

public class FindRoomByNumberQuery : IRequest<RoomDto?>
{
    public int Number { get; set; }


    class FindRoomByNumberQueryHandler : IRequestHandler<FindRoomByNumberQuery, RoomDto?>
    {
        private readonly IReadOnlyRepositoryAsync<Room> _repository;
        private readonly IMapper _mapper;

        public FindRoomByNumberQueryHandler(IReadOnlyRepositoryAsync<Room> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RoomDto?> Handle(FindRoomByNumberQuery request, CancellationToken cancellationToken)
        {
            var room = await _repository.FirstOrDefaultAsync(new GetRoomByNumberSpecification(request.Number));
            return _mapper.Map<RoomDto?>(room);
        }
    }
}
