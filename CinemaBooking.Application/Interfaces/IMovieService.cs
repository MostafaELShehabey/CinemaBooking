using CinemaBooking.Application.DTOs.Movies;

namespace CinemaBooking.Application.Interfaces;

public interface IMovieService
{
    Task<IReadOnlyList<MovieDto>> GetAllAsync();

    Task<MovieDto?> GetByIdAsync(int id);

    Task<MovieDto> CreateAsync(CreateMovieDto dto);

    Task<MovieDto> UpdateAsync(int id, UpdateMovieDto dto);

    Task DeleteAsync(int id);
}
