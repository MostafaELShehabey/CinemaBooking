using CinemaBooking.Application.DTOs.Screens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces.Services
{
    public interface IScreenService
    {
        Task<IReadOnlyList<ScreenDto>> GetAllAsync();

        Task<ScreenDto?> GetByIdAsync(int id);

        Task<IReadOnlyList<ScreenDto>> GetByCinemaIdAsync(
            int cinemaId);

        Task<ScreenDto> CreateAsync(CreateScreenDto dto);

        Task<ScreenDto> UpdateAsync(
            int id,
            UpdateScreenDto dto);

        Task DeleteAsync(int id);
    }
}
