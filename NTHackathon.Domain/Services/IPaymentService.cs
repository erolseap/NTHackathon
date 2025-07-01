using NTHackathon.Domain.DTOs;

namespace NTHackathon.Domain.Services;

public interface IPaymentService
{
    Task<PaymentResponseDto> CreateAsync(PaymentCreateDto dto);
    Task<bool> CheckPaymentAsync(PaymentCheckDto dto);
}
    