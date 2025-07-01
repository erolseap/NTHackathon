using MediatR;
using NTHackathon.Application.Repositories;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Commands;

public class CreateReservationCommand : IRequest<int>
{
    public required int CustomerId { get; init; }
    public required int RoomId { get; init; }
    public required DateTime CheckInDate { get; init; }
    public required DateTime CheckOutDate { get; init; }

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
    {
        private readonly IWriteRepositoryAsync<Reservation> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationCommandHandler(IWriteRepositoryAsync<Reservation> repository, IUnitOfWork unitOfWork)
        {
            _repository =  repository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = new()
            {
                CustomerId = request.CustomerId,
                RoomId = request.RoomId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
            };

            await _repository.AddAsync(reservation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reservation.Id;
        }
    }
}