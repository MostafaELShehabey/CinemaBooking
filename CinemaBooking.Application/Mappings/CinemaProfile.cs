using AutoMapper;
using CinemaBooking.Application.DTOs.Cinemas;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Mappings;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<Cinema, CinemaDto>();
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<UpdateCinemaDto, Cinema>();
    }
}
