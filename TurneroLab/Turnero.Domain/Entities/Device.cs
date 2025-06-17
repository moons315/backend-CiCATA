using System.Collections.Generic;

namespace Turnero.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Relación con Process
        public int ProcessId { get; set; }
        public Process Process { get; set; }

        // También vinculamos al laboratorio (por si un equipo puede moverse)
        public int LabId { get; set; }
        public Lab Lab { get; set; }

        // Duración estándar si no se especifica (en minutos)
        public int DurationMinutes { get; set; }

        // Para guardar configuraciones extra (JSON libre)
        public string ConfigJson { get; set; }

        // Un dispositivo puede tener muchas reservas
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
