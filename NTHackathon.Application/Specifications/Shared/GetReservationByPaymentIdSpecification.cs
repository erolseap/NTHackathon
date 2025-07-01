using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Specifications;

namespace NTHackathon.Application.Specifications.Shared;

public class GetReservationByPaymentIdSpecification : BaseSpecification<Reservation>
{
    public GetReservationByPaymentIdSpecification(int paymentId) : base(r => r.PaymentId == paymentId)
    {
    }
}
