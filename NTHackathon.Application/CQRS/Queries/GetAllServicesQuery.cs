using AutoMapper;
using MediatR;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Queries;

public class GetAllServicesQuery : IRequest<IReadOnlyList<ServiceDto>>
{
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IReadOnlyList<ServiceDto>>
    {
        private readonly IReadOnlyRepositoryAsync<Room> _repository;
        private readonly IMapper _mapper;

        public GetAllServicesQueryHandler(IReadOnlyRepositoryAsync<Room> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ServiceDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _repository.ListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<IReadOnlyList<ServiceDto>>(rooms);
        }
    }
}