using System;
using System.Collections.Generic;

namespace Turnero.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        // Usuario que reserva
        public int UserId { get; set; }
        public User User { get; set; }

        // Dispositivo reservado
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        // Fechas de inicio y fin
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Pendiente, Activa, Extendida, Finalizada, Cancelada
        public string Status { get; set; }

        // Observaciones libres
        public string Observations { get; set; }

        public DateTime CreatedAt { get; set; }

        // Posibles extensiones de esta reserva
        public ICollection<ReservationExtension> Extensions { get; set; }
            = new List<ReservationExtension>();
    }
}
