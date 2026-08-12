using CinemaBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public string BookingReference { get; set; } = string.Empty;

        public int ScreeningId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public DateTime BookingDate { get; set; }

        public decimal TotalAmount { get; set; }

        public BookingStatus Status { get; set; }

        // Navigation properties
        public Screening Screening { get; set; } = null!;

        public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
    }
}
