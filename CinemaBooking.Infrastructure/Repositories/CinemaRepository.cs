using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Infrastructure.Repositories;

public class CinemaRepository : ICinemaRepository
{
    private readonly ApplicationDbContext _context;

    public CinemaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cinema>> GetAllAsync()
    {
        return await _context.Cinemas
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Cinema?> GetByIdAsync(int id)
    {
        return await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Cinema cinema)
    {
        await _context.Cinemas.AddAsync(cinema);
    }

    public void Update(Cinema cinema)
    {
        _context.Cinemas.Update(cinema);
    }

    public void Delete(Cinema cinema)
    {
        _context.Cinemas.Remove(cinema);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Cinemas.AnyAsync(c => c.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
