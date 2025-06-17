using System;

namespace Turnero.API.DTOs
{
    public class ExtendReservationDto
    {
        public DateTime NewEndTime { get; set; }
        public string? Observations { get; set; }
    }
}
