using AutoMapper;
using CinemaBooking.Application.DTOs.Bookings;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Mappings;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.BookingSeats));

        CreateMap<BookingSeat, BookingSeatDto>()
            .ForMember(dest => dest.RowNumber, opt => opt.MapFrom(src => src.Seat.RowNumber))
            .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber));
    }
}
