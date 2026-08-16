namespace CinemaBooking.Application.DTOs.Seats;

public class SeatDto
{
    public int Id { get; set; }

    public int ScreenId { get; set; }

    public int RowNumber { get; set; }

    public int SeatNumber { get; set; }

    public bool IsActive { get; set; }
}
