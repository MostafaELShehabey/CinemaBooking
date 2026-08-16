using AutoMapper;
using CinemaBooking.Application.DTOs.Screens;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Mappings;

public class ScreenProfile : Profile
{
    public ScreenProfile()
    {
        CreateMap<Screen, ScreenDto>();
        CreateMap<CreateScreenDto, Screen>();
        CreateMap<UpdateScreenDto, Screen>();
    }
}
