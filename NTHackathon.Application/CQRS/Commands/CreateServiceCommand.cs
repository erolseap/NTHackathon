using MediatR;
using NTHackathon.Application.Repositories;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Repositories;

namespace NTHackathon.Application.CQRS.Commands;

public class CreateServiceCommand : IRequest<int>
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int ReservationId { get; init; }

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, int>
    {
        private readonly IWriteRepositoryAsync<ServiceEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceCommandHandler(IWriteRepositoryAsync<ServiceEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository =  repository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            ServiceEntity service = new()
            {
                Name = request.Name,
                Price = request.Price,
                ReservationId = request.ReservationId,
            };

            await _repository.AddAsync(service, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return service.Id;
        }
    }
}