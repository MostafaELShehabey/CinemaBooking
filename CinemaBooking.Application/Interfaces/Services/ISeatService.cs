using CinemaBooking.Application.DTOs.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces.Services
{
    public interface ISeatService
    {
        Task<IReadOnlyList<SeatDto>> GetAllAsync();

        Task<SeatDto?> GetByIdAsync(int id);

        Task<IReadOnlyList<SeatDto>> GetByScreenIdAsync(
            int screenId);

        Task<SeatDto> CreateAsync(CreateSeatDto dto);

        Task<SeatDto> UpdateAsync(
            int id,
            UpdateSeatDto dto);

        Task DeleteAsync(int id);
    }
}
