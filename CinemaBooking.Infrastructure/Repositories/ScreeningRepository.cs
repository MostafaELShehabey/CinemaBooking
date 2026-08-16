using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Infrastructure.Repositories;

public class ScreeningRepository : IScreeningRepository
{
    private readonly ApplicationDbContext _context;

    public ScreeningRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Screening>> GetAllAsync()
    {
        return await _context.Screenings
            .AsNoTracking()
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<Screening?> GetByIdAsync(int id)
    {
        return await _context.Screenings.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Screening>> GetByMovieIdAsync(int movieId)
    {
        return await _context.Screenings
            .AsNoTracking()
            .Where(s => s.MovieId == movieId)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Screening>> GetByScreenIdAsync(int screenId)
    {
        return await _context.Screenings
            .AsNoTracking()
            .Where(s => s.ScreenId == screenId)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task AddAsync(Screening screening)
    {
        await _context.Screenings.AddAsync(screening);
    }

    public void Update(Screening screening)
    {
        _context.Screenings.Update(screening);
    }

    public void Delete(Screening screening)
    {
        _context.Screenings.Remove(screening);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Screenings.AnyAsync(s => s.Id == id);
    }

    public async Task<bool> HasOverlapAsync(
        int screenId,
        DateTime startTime,
        DateTime endTime,
        int? exceptScreeningId = null)
    {
        return await _context.Screenings.AnyAsync(s =>
            s.ScreenId == screenId
            && s.IsActive
            && (!exceptScreeningId.HasValue || s.Id != exceptScreeningId.Value)
            && s.StartTime < endTime
            && startTime < s.EndTime);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
