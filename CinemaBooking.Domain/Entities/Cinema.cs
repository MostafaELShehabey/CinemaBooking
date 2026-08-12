using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Domain.Entities
{
    public class Cinema
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Screen> Screens { get; set; } = new List<Screen>();
    }
}
