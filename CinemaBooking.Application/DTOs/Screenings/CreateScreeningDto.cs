using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.DTOs.Screenings
{

    public class CreateScreeningDto
    {
        public int MovieId { get; set; }

        public int ScreenId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal Price { get; set; }
    }
}
