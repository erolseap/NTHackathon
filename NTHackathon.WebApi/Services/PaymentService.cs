using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NTHackathon.Application.Repositories;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;
using NTHackathon.Domain.Enums;
using NTHackathon.Domain.Repositories;
using NTHackathon.Domain.Services;

namespace NTHackathon.WebApi.Services;

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    private readonly PaymentConfigurationDto _paymentConfigurationDto;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IReadOnlyRepositoryAsync<Reservation> _reservationDto;
    private readonly IWriteRepositoryAsync<Reservation> _reservationDtoRw;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IConfiguration configuration, IHttpContextAccessor contextAccessor, IHttpClientFactory httpClientFactory, IReadOnlyRepositoryAsync<Reservation> reservationDto, IWriteRepositoryAsync<Reservation> reservationDtoRw, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _paymentConfigurationDto = _configuration.GetSection("PaymentSettings").Get<PaymentConfigurationDto>() ?? new();
        _contextAccessor = contextAccessor;
        _httpClientFactory = httpClientFactory;
        _reservationDto = reservationDto;
        _reservationDtoRw = reservationDtoRw;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CheckPaymentAsync(PaymentCheckDto dto)
    {
        var reservation = await _reservationDtoRw.SingleOrDefaultAsync(new GetReservationByPaymentIdSpecification(dto.Id));
        if (reservation == null)
        {
            return false;
        }

        if (dto.Status == PaymentStatus.FullyPaid || dto.Status == PaymentStatus.Declined)
        {
            reservation.IsPaid = true;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<PaymentResponseDto> CreateAsync(PaymentCreateDto dto)
    {
        string confirmToken = Guid.NewGuid().ToString();

        UrlActionContext context = new()
        {
            Action = "CheckPayment",
            Controller = "Order",
            Values = new { Token = confirmToken },
            Protocol = _contextAccessor.HttpContext?.Request.Scheme
        };

        var redirectUrl = "http://localhost:5173/payment";

        string selectedCulture = "az";

        string amount = dto.Amount.ToString().Replace(',', '.');


        string requestBody = $@"
    {{
        ""order"": {{
            ""typeRid"": ""Order_SMS"",
            ""amount"": {amount},
            ""currency"": ""AZN"",
            ""language"": ""{selectedCulture}"",
            ""description"": ""{dto.Description}"",
            ""hppRedirectUrl"": ""{redirectUrl}"",
            ""hppCofCapturePurposes"": [""Cit""]
        }}
    }}";

        var httpClient = _httpClientFactory.CreateClient("KapitalBankClient");
        var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_paymentConfigurationDto.Username}:{_paymentConfigurationDto.Password}"));
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(_paymentConfigurationDto.BaseUrl, content);

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.StatusCode.ToString());

        var responseContent = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<PaymentResponseDto>(responseContent) ?? new();

        return result;
    }
}
