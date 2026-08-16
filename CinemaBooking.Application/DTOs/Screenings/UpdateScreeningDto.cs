namespace CinemaBooking.Application.DTOs.Screenings;

public class UpdateScreeningDto
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
}
