using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Specifications;

namespace NTHackathon.Application.Specifications.Shared;

public class GetRoomByCustomerIdSpecification : BaseSpecification<Room>
{
    public GetRoomByCustomerIdSpecification(int customerId)
        : base(r => r.Id == customerId)
    {
    }
}
