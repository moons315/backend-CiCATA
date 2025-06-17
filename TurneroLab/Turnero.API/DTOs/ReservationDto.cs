using System;
using System.Collections.Generic;

namespace Turnero.API.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public int DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public int LabId { get; set; }
        public string? LabName { get; set; }
        public int ProcessId { get; set; }
        public string? ProcessName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Observations { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<ReservationExtensionDto>? Extensions { get; set; }
    }

    public class ReservationExtensionDto
    {
        public int Id { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime NewEndTime { get; set; }
        public bool Approved { get; set; }
        public string? Observations { get; set; }
    }
}
