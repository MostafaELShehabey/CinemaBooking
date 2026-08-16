namespace CinemaBooking.Application.DTOs.Screenings;

public class ScreeningDto
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public int ScreenId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
}
