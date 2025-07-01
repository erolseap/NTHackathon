using MediatR;
using NTHackathon.Application.Repositories;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Enums;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Commands
{
    public class CreateRoomCommand : IRequest<int>
    {
        public required int Number { get; init; }
        public required RoomType Type { get; init; }
        public required decimal PricePerNight { get; init; }
        public required bool IsReserved { get; init; }


        class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, int>
        {
            private readonly IWriteRepositoryAsync<Room> _repository;
            private readonly IUnitOfWork _unitOfWork;
            public CreateRoomCommandHandler(IWriteRepositoryAsync<Room> roomRepository, IUnitOfWork unitOfWork)
            {
                _repository = roomRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
            {
                Room room = new()
                {
                    Number = request.Number,
                    Type = request.Type,
                    IsReserved = request.IsReserved,
                    PricePerNight = request.PricePerNight,
                };

                await _repository.AddAsync(room, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return room.Id;
            }
        }

    }
}
