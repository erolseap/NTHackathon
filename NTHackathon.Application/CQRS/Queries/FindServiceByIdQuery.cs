using AutoMapper;
using MediatR;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Queries;

public class FindServiceByIdQuery : IRequest<ServiceDto?>
{
    public required int Id { get; set; }

    class FindServiceByIdQueryHandler : IRequestHandler<FindServiceByIdQuery, ServiceDto?>
    {
        private readonly IReadOnlyRepositoryAsync<ServiceEntity> _repository;
        private readonly IMapper _mapper;

        public FindServiceByIdQueryHandler(IReadOnlyRepositoryAsync<ServiceEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceDto?> Handle(FindServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _repository.FirstOrDefaultAsync(new GetEntityByIdSpecification<ServiceEntity>(request.Id), cancellationToken);
            return _mapper.Map<ServiceDto?>(service);
        }
    }
}
