using AutoMapper;
using CinemaBooking.Application.Common;
using CinemaBooking.Application.DTOs.Bookings;
using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Enums;

namespace CinemaBooking.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IBookingSeatRepository _bookingSeatRepository;
    private readonly IScreeningRepository _screeningRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IMapper _mapper;

    public BookingService(
        IBookingRepository bookingRepository,
        IBookingSeatRepository bookingSeatRepository,
        IScreeningRepository screeningRepository,
        ISeatRepository seatRepository,
        IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _bookingSeatRepository = bookingSeatRepository;
        _screeningRepository = screeningRepository;
        _seatRepository = seatRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<BookingDto>> GetAllAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<BookingDto>>(bookings);
    }

    public async Task<BookingDto?> GetByIdAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdWithSeatsAsync(id);
        return booking is null ? null : _mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto> CreateAsync(CreateBookingDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CustomerName) || string.IsNullOrWhiteSpace(dto.CustomerEmail))
        {
            throw new BusinessRuleException("Customer name and email are required.");
        }

        if (dto.SeatIds is null || dto.SeatIds.Count == 0)
        {
            throw new BusinessRuleException("At least one seat is required.");
        }

        if (dto.SeatIds.Distinct().Count() != dto.SeatIds.Count)
        {
            throw new BusinessRuleException("Duplicate seat ids are not allowed.");
        }

        var screening = await _screeningRepository.GetByIdAsync(dto.ScreeningId)
            ?? throw new NotFoundException(nameof(Screening), dto.ScreeningId);

        if (!screening.IsActive)
        {
            throw new BusinessRuleException("This screening is not available for booking.");
        }

        var seats = await ResolveSeatsForScreeningAsync(screening, dto.SeatIds);

        foreach (var seat in seats)
        {
            if (await _bookingSeatRepository.IsSeatBookedAsync(screening.Id, seat.Id))
            {
                throw new BusinessRuleException(
                    $"Seat {seat.RowNumber}-{seat.SeatNumber} is already booked for this screening.");
            }
        }

        var booking = new Booking
        {
            BookingReference = GenerateReference(),
            ScreeningId = screening.Id,
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            BookingDate = DateTime.UtcNow,
            TotalAmount = screening.Price * seats.Count,
            Status = BookingStatus.Confirmed
        };

        foreach (var seat in seats)
        {
            booking.BookingSeats.Add(new BookingSeat
            {
                SeatId = seat.Id,
                UnitPrice = screening.Price
            });
        }

        await _bookingRepository.AddAsync(booking);
        await _bookingRepository.SaveChangesAsync();

        var created = await _bookingRepository.GetByIdWithSeatsAsync(booking.Id)
            ?? throw new InvalidOperationException("Booking was created but could not be reloaded.");
        return _mapper.Map<BookingDto>(created);
    }

    public async Task<BookingDto> UpdateAsync(int id, UpdateBookingDto dto)
    {
        var booking = await _bookingRepository.GetByIdWithSeatsAsync(id)
            ?? throw new NotFoundException(nameof(Booking), id);

        if (booking.Status == BookingStatus.Cancelled)
        {
            throw new BusinessRuleException("A cancelled booking cannot be updated.");
        }

        if (dto.SeatIds is null || dto.SeatIds.Count == 0)
        {
            throw new BusinessRuleException("At least one seat is required.");
        }

        if (dto.SeatIds.Distinct().Count() != dto.SeatIds.Count)
        {
            throw new BusinessRuleException("Duplicate seat ids are not allowed.");
        }

        var screening = await _screeningRepository.GetByIdAsync(booking.ScreeningId)
            ?? throw new NotFoundException(nameof(Screening), booking.ScreeningId);

        var seats = await ResolveSeatsForScreeningAsync(screening, dto.SeatIds);

        foreach (var seat in seats)
        {
            if (await _bookingSeatRepository.IsSeatBookedAsync(screening.Id, seat.Id, booking.Id))
            {
                throw new BusinessRuleException(
                    $"Seat {seat.RowNumber}-{seat.SeatNumber} is already booked for this screening.");
            }
        }

        _bookingSeatRepository.RemoveRange(booking.BookingSeats.ToList());
        booking.BookingSeats.Clear();

        foreach (var seat in seats)
        {
            booking.BookingSeats.Add(new BookingSeat
            {
                BookingId = booking.Id,
                SeatId = seat.Id,
                UnitPrice = screening.Price
            });
        }

        booking.TotalAmount = screening.Price * seats.Count;
        _bookingRepository.Update(booking);
        await _bookingRepository.SaveChangesAsync();

        var updated = await _bookingRepository.GetByIdWithSeatsAsync(booking.Id)
            ?? throw new InvalidOperationException("Booking was updated but could not be reloaded.");
        return _mapper.Map<BookingDto>(updated);
    }

    public async Task<BookingDto> CancelAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdWithSeatsAsync(id)
            ?? throw new NotFoundException(nameof(Booking), id);

        if (booking.Status == BookingStatus.Cancelled)
        {
            throw new BusinessRuleException("This booking is already cancelled.");
        }

        booking.Status = BookingStatus.Cancelled;
        _bookingRepository.Update(booking);
        await _bookingRepository.SaveChangesAsync();
        return _mapper.Map<BookingDto>(booking);
    }

    private async Task<List<Seat>> ResolveSeatsForScreeningAsync(Screening screening, IEnumerable<int> seatIds)
    {
        var seats = new List<Seat>();

        foreach (var seatId in seatIds)
        {
            var seat = await _seatRepository.GetByIdAsync(seatId)
                ?? throw new NotFoundException(nameof(Seat), seatId);

            if (seat.ScreenId != screening.ScreenId)
            {
                throw new BusinessRuleException($"Seat {seatId} does not belong to this screening's screen.");
            }

            if (!seat.IsActive)
            {
                throw new BusinessRuleException($"Seat {seat.RowNumber}-{seat.SeatNumber} is not available.");
            }

            seats.Add(seat);
        }

        return seats;
    }

    private static string GenerateReference()
    {
        return $"CB-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";
    }
}
