using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Infrastructure.Repositories;

public class ScreenRepository : IScreenRepository
{
    private readonly ApplicationDbContext _context;

    public ScreenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Screen>> GetAllAsync()
    {
        return await _context.Screens
            .AsNoTracking()
            .OrderBy(s => s.CinemaId)
            .ThenBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<Screen?> GetByIdAsync(int id)
    {
        return await _context.Screens.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Screen>> GetByCinemaIdAsync(int cinemaId)
    {
        return await _context.Screens
            .AsNoTracking()
            .Where(s => s.CinemaId == cinemaId)
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Screen screen)
    {
        await _context.Screens.AddAsync(screen);
    }

    public void Update(Screen screen)
    {
        _context.Screens.Update(screen);
    }

    public void Delete(Screen screen)
    {
        _context.Screens.Remove(screen);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Screens.AnyAsync(s => s.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
