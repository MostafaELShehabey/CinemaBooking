using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Enums;
using CinemaBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Infrastructure.Repositories;

public class BookingSeatRepository : IBookingSeatRepository
{
    private readonly ApplicationDbContext _context;

    public BookingSeatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookingSeat>> GetByBookingIdAsync(int bookingId)
    {
        return await _context.BookingSeats
            .Include(bs => bs.Seat)
            .Where(bs => bs.BookingId == bookingId)
            .ToListAsync();
    }

    public async Task<IEnumerable<BookingSeat>> GetByScreeningIdAsync(int screeningId)
    {
        return await _context.BookingSeats
            .Include(bs => bs.Booking)
            .Where(bs => bs.Booking.ScreeningId == screeningId
                         && bs.Booking.Status != BookingStatus.Cancelled)
            .ToListAsync();
    }

    public async Task<bool> IsSeatBookedAsync(int screeningId, int seatId, int? exceptBookingId = null)
    {
        return await _context.BookingSeats
            .AnyAsync(bs =>
                bs.SeatId == seatId
                && bs.Booking.ScreeningId == screeningId
                && bs.Booking.Status != BookingStatus.Cancelled
                && (!exceptBookingId.HasValue || bs.BookingId != exceptBookingId.Value));
    }

    public async Task AddAsync(BookingSeat bookingSeat)
    {
        await _context.BookingSeats.AddAsync(bookingSeat);
    }

    public async Task AddRangeAsync(IEnumerable<BookingSeat> bookingSeats)
    {
        await _context.BookingSeats.AddRangeAsync(bookingSeats);
    }

    public void RemoveRange(IEnumerable<BookingSeat> bookingSeats)
    {
        _context.BookingSeats.RemoveRange(bookingSeats);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
