using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }

        public int ScreenId { get; set; }

        public int RowNumber { get; set; }

        public int SeatNumber { get; set; }

        public bool IsActive { get; set; }

        // Navigation properties
        public Screen Screen { get; set; } = null!;

        public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
    }
}
