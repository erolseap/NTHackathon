using MediatR;
using NTHackathon.Application.Repositories;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Commands;

public class RemoveReservationCommand : IRequest
{
    public required int Id { get; init; }

    public class RemoveReservationCommandHandler : IRequestHandler<RemoveReservationCommand>
    {
        private readonly IWriteRepositoryAsync<Reservation> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveReservationCommandHandler(IWriteRepositoryAsync<Reservation> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task Handle(RemoveReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.SingleOrDefaultAsync(new GetEntityByIdSpecification<Reservation>(request.Id), cancellationToken);
            if (reservation != null)
            {
                _repository.Remove(reservation);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
