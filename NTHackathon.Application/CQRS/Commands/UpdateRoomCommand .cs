using MediatR;
using NTHackathon.Application.Models;
using NTHackathon.Application.Repositories;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Enums;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Commands;

public class UpdateRoomCommand : IRequest<Result>
{
    public required int Id { get; init; }

    public RoomType? Type { get; init; }
    public decimal? PricePerNight { get; init; }
    public bool? IsReserved { get; init; }


    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Result>
    {
        private readonly IWriteRepositoryAsync<Room> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomCommandHandler(IWriteRepositoryAsync<Room> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _repository.SingleOrDefaultAsync(new GetEntityByIdSpecification<Room>(request.Id), cancellationToken);
            if (room == null)
            {
                return Result.Failure($"A {nameof(Room)} with id '{request.Id}' was not found");
            }


            if (request.PricePerNight != null)
            {
                room.PricePerNight = request.PricePerNight.Value;
            }

            if (request.Type != null)
            {
                room.Type = request.Type.Value;
            }

            if (request.IsReserved != null)
            {
                room.IsReserved = request.IsReserved.Value;
            }


            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}

