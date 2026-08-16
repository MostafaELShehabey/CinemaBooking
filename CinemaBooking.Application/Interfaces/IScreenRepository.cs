using CinemaBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces
{
    public interface IScreenRepository
    {
        Task<IEnumerable<Screen>> GetAllAsync();

        Task<Screen?> GetByIdAsync(int id);

        Task<IEnumerable<Screen>> GetByCinemaIdAsync(int cinemaId);

        Task AddAsync(Screen screen);

        void Update(Screen screen);

        void Delete(Screen screen);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
