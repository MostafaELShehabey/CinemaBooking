using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Interfaces;

public interface IBookingSeatRepository
{
    Task<IEnumerable<BookingSeat>> GetByBookingIdAsync(int bookingId);

    Task<IEnumerable<BookingSeat>> GetByScreeningIdAsync(int screeningId);

    Task<bool> IsSeatBookedAsync(int screeningId, int seatId, int? exceptBookingId = null);

    Task AddAsync(BookingSeat bookingSeat);

    Task AddRangeAsync(IEnumerable<BookingSeat> bookingSeats);

    void RemoveRange(IEnumerable<BookingSeat> bookingSeats);

    Task SaveChangesAsync();
}
