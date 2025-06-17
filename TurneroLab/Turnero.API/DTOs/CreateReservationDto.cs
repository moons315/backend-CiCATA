using System;

namespace Turnero.API.DTOs
{
    public class CreateReservationDto
    {
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Observations { get; set; }
    }
}
