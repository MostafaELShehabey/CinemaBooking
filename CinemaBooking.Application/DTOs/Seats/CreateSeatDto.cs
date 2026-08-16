namespace CinemaBooking.Application.DTOs.Seats;

public class CreateSeatDto
{
    public int ScreenId { get; set; }

    public int RowNumber { get; set; }

    public int SeatNumber { get; set; }
}
