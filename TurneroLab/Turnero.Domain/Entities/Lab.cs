using System.Collections.Generic;

namespace Turnero.Domain.Entities
{
    public class Lab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        // Un laboratorio contiene procesos y dispositivos
        public ICollection<Process> Processes { get; set; } = new List<Process>();
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
