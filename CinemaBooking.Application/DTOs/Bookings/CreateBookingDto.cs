namespace CinemaBooking.Application.DTOs.Bookings;

public class CreateBookingDto
{
    public int ScreeningId { get; set; }

    public List<int> SeatIds { get; set; } = new();
}
