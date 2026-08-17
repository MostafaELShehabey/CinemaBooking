using CinemaBooking.Application.DTOs.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<IReadOnlyList<BookingDto>> GetAllAsync();

        Task<BookingDto?> GetByIdAsync(int id);

        Task<BookingDto> CreateAsync(CreateBookingDto dto);

        Task<BookingDto> UpdateAsync(
            int id,
            UpdateBookingDto dto);

        Task<BookingDto> CancelAsync(int id);
    }
}
