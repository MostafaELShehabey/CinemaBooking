using CinemaBooking.Application.DTOs.Bookings;

namespace CinemaBooking.Application.Interfaces;

public interface IBookingService
{
    Task<IReadOnlyList<BookingDto>> GetAllAsync();

    Task<BookingDto?> GetByIdAsync(int id);

    Task<BookingDto> CreateAsync(CreateBookingDto dto);

    Task<BookingDto> UpdateAsync(int id, UpdateBookingDto dto);

    Task<BookingDto> CancelAsync(int id);
}
