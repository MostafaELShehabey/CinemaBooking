using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public string Genre { get; set; } = string.Empty;

        public string AgeRating { get; set; } = string.Empty;

        public string? PosterUrl { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation property
        public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
    }
}
