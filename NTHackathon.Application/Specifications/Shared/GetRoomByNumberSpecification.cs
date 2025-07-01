using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Specifications;

namespace NTHackathon.Application.Specifications.Shared
{
    public class GetRoomByNumberSpecification : BaseSpecification<Room>
    {
        public GetRoomByNumberSpecification(int number) : base(p => p.Number == number)
        {

        }
    }
}
