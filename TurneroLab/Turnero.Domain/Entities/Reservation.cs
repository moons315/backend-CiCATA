using System;
using System.Collections.Generic;

namespace Turnero.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int DeviceId { get; set; }
        public Device Device { get; set; } = null!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ReservationStatus Status { get; set; }

        public bool CancelledByAdmin { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public string? Observations { get; set; }
        public ICollection<ReservationExtension> Extensions { get; set; } = new List<ReservationExtension>();
    }
}
