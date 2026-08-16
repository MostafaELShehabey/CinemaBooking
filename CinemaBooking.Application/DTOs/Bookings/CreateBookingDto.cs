namespace CinemaBooking.Application.DTOs.Bookings;

public class CreateBookingDto
{
    public int ScreeningId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public List<int> SeatIds { get; set; } = new();
}
