using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.DTOs.Bookings
{
    public class UpdateBookingDto
    {
        public List<int> SeatIds { get; set; } = new();
    }
}
