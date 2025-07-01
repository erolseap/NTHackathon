using MediatR;
using NTHackathon.Application.Repositories;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Commands;

public class RemoveServiceCommand : IRequest
{
    public required int Id { get; init; }

    public class RemoveServiceCommandHandler : IRequestHandler<RemoveServiceCommand>
    {
        private readonly IWriteRepositoryAsync<ServiceEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveServiceCommandHandler(IWriteRepositoryAsync<ServiceEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.SingleOrDefaultAsync(new GetEntityByIdSpecification<ServiceEntity>(request.Id), cancellationToken);
            if (reservation != null)
            {
                _repository.Remove(reservation);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
