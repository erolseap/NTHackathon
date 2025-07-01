using AutoMapper;
using MediatR;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Queries;

public class FindRoomByReservedQuery : IRequest<IReadOnlyList<RoomDto>>
{
    public bool IsReserved { get; set; }


    class FindRoomByReservedQueryHandler : IRequestHandler<FindRoomByReservedQuery, IReadOnlyList<RoomDto>>
    {
        private readonly IReadOnlyRepositoryAsync<Room> _repository;
        private readonly IMapper _mapper;

        public FindRoomByReservedQueryHandler(IReadOnlyRepositoryAsync<Room> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<RoomDto>> Handle(FindRoomByReservedQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _repository.ListAsync(new GetRoomByReservedSpecification(request.IsReserved));
            return _mapper.Map<IReadOnlyList<RoomDto>>(rooms);
        }
    }
}
