using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Infrastructure.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly ApplicationDbContext _context;

    public SeatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Seat>> GetAllAsync()
    {
        return await _context.Seats
            .AsNoTracking()
            .OrderBy(s => s.ScreenId)
            .ThenBy(s => s.RowNumber)
            .ThenBy(s => s.SeatNumber)
            .ToListAsync();
    }

    public async Task<Seat?> GetByIdAsync(int id)
    {
        return await _context.Seats.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Seat>> GetByScreenIdAsync(int screenId)
    {
        return await _context.Seats
            .Where(s => s.ScreenId == screenId)
            .OrderBy(s => s.RowNumber)
            .ThenBy(s => s.SeatNumber)
            .ToListAsync();
    }

    public async Task AddAsync(Seat seat)
    {
        await _context.Seats.AddAsync(seat);
    }

    public void Update(Seat seat)
    {
        _context.Seats.Update(seat);
    }

    public void Delete(Seat seat)
    {
        _context.Seats.Remove(seat);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Seats.AnyAsync(s => s.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
