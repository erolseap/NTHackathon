using AutoMapper;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;

namespace NTHackathon.Application.Profiles;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Reservation, ReservationDto>();
    }
}