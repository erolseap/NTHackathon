using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Specifications;

namespace NTHackathon.Application.Specifications.Shared;

public class GetRoomByPriceSpecification : BaseSpecification<Room>
{
    public GetRoomByPriceSpecification(decimal? minPrice, decimal? maxPrice) : base(room =>
            (!minPrice.HasValue || room.PricePerNight >= minPrice.Value) &&
            (!maxPrice.HasValue || room.PricePerNight <= maxPrice.Value))
    {
    }
}
