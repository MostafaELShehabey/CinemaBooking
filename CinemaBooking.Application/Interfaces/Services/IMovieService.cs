using CinemaBooking.Application.DTOs.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces.Services
{

    public interface IMovieService
    {
        Task<IReadOnlyList<MovieDto>> GetAllAsync();

        Task<MovieDto?> GetByIdAsync(int id);

        Task<MovieDto> CreateAsync(CreateMovieDto dto);

        Task<MovieDto> UpdateAsync(
            int id,
            UpdateMovieDto dto);

        Task DeleteAsync(int id);
    }
}
