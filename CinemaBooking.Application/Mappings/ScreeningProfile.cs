using AutoMapper;
using CinemaBooking.Application.DTOs.Screenings;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Mappings;

public class ScreeningProfile : Profile
{
    public ScreeningProfile()
    {
        CreateMap<Screening, ScreeningDto>();
        CreateMap<CreateScreeningDto, Screening>();
        CreateMap<UpdateScreeningDto, Screening>();
    }
}
