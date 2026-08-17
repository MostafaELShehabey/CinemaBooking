using CinemaBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces.Repositories
{
    public interface ICinemaRepository
    {
        Task<IEnumerable<Cinema>> GetAllAsync();

        Task<Cinema?> GetByIdAsync(int id);

        Task AddAsync(Cinema cinema);

        void Update(Cinema cinema);

        void Delete(Cinema cinema);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
