using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.DTOs.Screens
{
    public class ScreenDto
    {
        public int Id { get; set; }

        public int CinemaId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
