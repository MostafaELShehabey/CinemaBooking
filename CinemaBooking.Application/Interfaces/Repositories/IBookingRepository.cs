using CinemaBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();

        Task<Booking?> GetByIdAsync(int id);

        Task<Booking?> GetByIdWithSeatsAsync(int id);

        Task AddAsync(Booking booking);

        void Update(Booking booking);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
