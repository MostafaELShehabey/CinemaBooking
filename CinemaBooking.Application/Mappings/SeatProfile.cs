using AutoMapper;
using CinemaBooking.Application.DTOs.Seats;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Mappings;

public class SeatProfile : Profile
{
    public SeatProfile()
    {
        CreateMap<Seat, SeatDto>();
        CreateMap<CreateSeatDto, Seat>();
        CreateMap<UpdateSeatDto, Seat>();
    }
}
