namespace CinemaBooking.Application.DTOs.Bookings;

public class BookingSeatDto
{
    public int Id { get; set; }

    public int SeatId { get; set; }

    public int RowNumber { get; set; }

    public int SeatNumber { get; set; }

    public decimal UnitPrice { get; set; }
}
