using CinemaBooking.Application.DTOs.Seats;

namespace CinemaBooking.Application.Interfaces;

public interface ISeatService
{
    Task<IReadOnlyList<SeatDto>> GetAllAsync();

    Task<SeatDto?> GetByIdAsync(int id);

    Task<IReadOnlyList<SeatDto>> GetByScreenIdAsync(int screenId);

    Task<SeatDto> CreateAsync(CreateSeatDto dto);

    Task<SeatDto> UpdateAsync(int id, UpdateSeatDto dto);

    Task DeleteAsync(int id);
}
