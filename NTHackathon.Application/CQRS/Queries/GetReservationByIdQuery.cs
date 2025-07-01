using AutoMapper;
using MediatR;
using NTHackathon.Application.Repositories;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Queries;

public class GetReservationByIdQuery : IRequest<ReservationDto?>
{
    public required int Id { get; init; }

    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationDto?>
    {
        private readonly IReadOnlyRepositoryAsync<Reservation> _repository;
        private readonly IMapper _mapper;

        public GetReservationByIdQueryHandler(IReadOnlyRepositoryAsync<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<ReservationDto?> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.SingleOrDefaultAsync(new GetEntityByIdSpecification<Reservation>(request.Id), cancellationToken);
            return _mapper.Map<ReservationDto?>(reservation);
        }
    }
}