using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Specifications;

namespace NTHackathon.Application.Specifications.Shared;

public class GetRoomByReservedSpecification : BaseSpecification<Room>
{
    public GetRoomByReservedSpecification(bool isReserved) : base(p => p.IsReserved == isReserved)
    {

    }
}
