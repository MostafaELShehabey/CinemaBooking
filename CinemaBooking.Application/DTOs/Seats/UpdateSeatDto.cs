namespace CinemaBooking.Application.DTOs.Seats;

public class UpdateSeatDto
{
    public int RowNumber { get; set; }

    public int SeatNumber { get; set; }

    public bool IsActive { get; set; }
}
