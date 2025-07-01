using AutoMapper;
using NTHackathon.Domain.DTOs;
using NTHackathon.Domain.Entities;

namespace NTHackathon.Application.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDto>();
        }
    }
}
