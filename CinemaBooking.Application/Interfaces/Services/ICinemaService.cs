using CinemaBooking.Application.DTOs.Cinemas;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces.Services
{
    public interface ICinemaService
    {
        Task<IReadOnlyList<CinemaDto>> GetAllAsync();

        Task<CinemaDto?> GetByIdAsync(int id);

        Task<CinemaDto> CreateAsync(CreateCinemaDto dto);

        Task<CinemaDto> UpdateAsync(
            int id,
            UpdateCinemaDto dto);

        Task DeleteAsync(int id);
    }
}
