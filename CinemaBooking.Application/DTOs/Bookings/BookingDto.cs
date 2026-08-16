namespace CinemaBooking.Application.DTOs.Bookings;

public class BookingDto
{
    public int Id { get; set; }

    public string BookingReference { get; set; } = string.Empty;

    public int ScreeningId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public List<BookingSeatDto> Seats { get; set; } = new();
}
