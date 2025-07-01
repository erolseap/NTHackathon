using AutoMapper;
using MediatR;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Queries;

public class GetAllReservationsQuery : IRequest<IReadOnlyList<ReservationDto>>
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, IReadOnlyList<ReservationDto>>
    {
        private readonly IReadOnlyRepositoryAsync<Reservation> _repository;
        private readonly IMapper _mapper;

        public GetAllReservationsQueryHandler(IReadOnlyRepositoryAsync<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<IReadOnlyList<ReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _repository.ListAsync(new FetchAllEntitiesSpecification<Reservation>(), cancellationToken);
            return _mapper.Map<IReadOnlyList<ReservationDto>>(reservations);
        }
    }
}
