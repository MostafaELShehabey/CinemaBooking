using CinemaBooking.Application.DTOs.Screens;

namespace CinemaBooking.Application.Interfaces;

public interface IScreenService
{
    Task<IReadOnlyList<ScreenDto>> GetAllAsync();

    Task<ScreenDto?> GetByIdAsync(int id);

    Task<IReadOnlyList<ScreenDto>> GetByCinemaIdAsync(int cinemaId);

    Task<ScreenDto> CreateAsync(CreateScreenDto dto);

    Task<ScreenDto> UpdateAsync(int id, UpdateScreenDto dto);

    Task DeleteAsync(int id);
}
