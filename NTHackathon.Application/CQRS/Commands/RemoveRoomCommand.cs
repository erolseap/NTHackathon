using MediatR;
using NTHackathon.Application.Repositories;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Commands;

public class RemoveRoomCommand : IRequest
{
    public required int Id { get; init; }

    public class RemoveRoomCommandHandler : IRequestHandler<RemoveRoomCommand>
    {
        private readonly IWriteRepositoryAsync<Room> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRoomCommandHandler(IWriteRepositoryAsync<Room> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveRoomCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.SingleOrDefaultAsync(new GetEntityByIdSpecification<Room>(request.Id), cancellationToken);
            if (reservation != null)
            {
                _repository.Remove(reservation);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
