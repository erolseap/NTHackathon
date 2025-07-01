using AutoMapper;
using MediatR;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Queries;

public class FindRoomsByCustomerIdQuery : IRequest<IReadOnlyList<RoomDto>>
{
    public int CustomerId { get; set; }

    public FindRoomsByCustomerIdQuery(int customerId)
    {
        CustomerId = customerId;
    }

    public class FindRoomByCustomerIdQueryHandler : IRequestHandler<FindRoomsByCustomerIdQuery, IReadOnlyList<RoomDto>>
    {
        private readonly IReadOnlyRepositoryAsync<Room> _repository;
        private readonly IMapper _mapper;

        public FindRoomByCustomerIdQueryHandler(IReadOnlyRepositoryAsync<Room> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<RoomDto>> Handle(FindRoomsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetRoomByCustomerIdSpecification(request.CustomerId);
            var rooms = await _repository.ListAsync(spec);
            return _mapper.Map<IReadOnlyList<RoomDto>>(rooms);
        }
    }
}

