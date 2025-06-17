using System.Collections.Generic;

namespace Turnero.Domain.Entities
{
    public class Process
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Cada proceso pertenece a un laboratorio
        public int LabId { get; set; }
        public Lab Lab { get; set; }

        // Duración por defecto en minutos (2 h = 120, 4 h = 240…)
        public int DefaultDurationMinutes { get; set; }

        // Un proceso incluye varios dispositivos
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
