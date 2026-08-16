using CinemaBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces
{

    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetAllAsync();

        Task<Seat?> GetByIdAsync(int id);

        Task<IEnumerable<Seat>> GetByScreenIdAsync(int screenId);

        Task AddAsync(Seat seat);

        void Update(Seat seat);

        void Delete(Seat seat);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
