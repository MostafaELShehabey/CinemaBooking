using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Domain.Entities
{
    public class Screen
    {
        public int Id { get; set; }

        public int CinemaId { get; set; }

        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public Cinema Cinema { get; set; } = null!;

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

        public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
    }
}
