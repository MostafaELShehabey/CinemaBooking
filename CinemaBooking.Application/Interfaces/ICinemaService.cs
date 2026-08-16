using CinemaBooking.Application.DTOs.Cinemas;

namespace CinemaBooking.Application.Interfaces;

public interface ICinemaService
{
    Task<IReadOnlyList<CinemaDto>> GetAllAsync();

    Task<CinemaDto?> GetByIdAsync(int id);

    Task<CinemaDto> CreateAsync(CreateCinemaDto dto);

    Task<CinemaDto> UpdateAsync(int id, UpdateCinemaDto dto);

    Task DeleteAsync(int id);
}
