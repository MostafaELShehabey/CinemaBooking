using CinemaBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces
{
    public interface IScreeningRepository
    {
        Task<IEnumerable<Screening>> GetAllAsync();

        Task<Screening?> GetByIdAsync(int id);

        Task<IEnumerable<Screening>> GetByMovieIdAsync(int movieId);

        Task<IEnumerable<Screening>> GetByScreenIdAsync(int screenId);

        Task AddAsync(Screening screening);

        void Update(Screening screening);

        void Delete(Screening screening);

        Task<bool> ExistsAsync(int id);

        Task<bool> HasOverlapAsync(int screenId, DateTime startTime, DateTime endTime, int? exceptScreeningId = null);

        Task SaveChangesAsync();
    }
}
