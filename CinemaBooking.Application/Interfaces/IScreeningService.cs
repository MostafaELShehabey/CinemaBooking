using CinemaBooking.Application.DTOs.Screenings;
using CinemaBooking.Application.DTOs.Seats;

namespace CinemaBooking.Application.Interfaces;

public interface IScreeningService
{
    Task<IReadOnlyList<ScreeningDto>> GetAllAsync();

    Task<ScreeningDto?> GetByIdAsync(int id);

    Task<IReadOnlyList<ScreeningDto>> GetByMovieIdAsync(int movieId);

    Task<IReadOnlyList<ScreeningDto>> GetByScreenIdAsync(int screenId);

    Task<IReadOnlyList<SeatDto>> GetAvailableSeatsAsync(int screeningId);

    Task<ScreeningDto> CreateAsync(CreateScreeningDto dto);

    Task<ScreeningDto> UpdateAsync(int id, UpdateScreeningDto dto);

    Task DeleteAsync(int id);
}
