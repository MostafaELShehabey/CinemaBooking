using CinemaBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();

        Task<Movie?> GetByIdAsync(int id);

        Task AddAsync(Movie movie);

        void Update(Movie movie);

        void Delete(Movie movie);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
