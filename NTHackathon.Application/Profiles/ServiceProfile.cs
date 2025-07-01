using AutoMapper;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;

namespace NTHackathon.Application.Profiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<ServiceEntity, ServiceDto>();
    }
}