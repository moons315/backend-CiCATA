using System;

namespace Turnero.Domain.Entities
{
    public class ReservationExtension
    {
        public int Id { get; set; }

        // A cuál reserva pertenece
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime NewEndTime { get; set; }

        // Si la extensión fue aprobada o no
        public bool Approved { get; set; }

        public string Observations { get; set; }
    }
}
