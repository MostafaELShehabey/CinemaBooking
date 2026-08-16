namespace CinemaBooking.Application.DTOs.Movies;

public class UpdateMovieDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    public string Genre { get; set; } = string.Empty;

    public string AgeRating { get; set; } = string.Empty;

    public string? PosterUrl { get; set; }

    public bool IsActive { get; set; }
}
