using System;

namespace Turnero.Domain.Entities
{
    public class Holiday
    {
        public int Id { get; set; }

        // Fecha en formato yyyy-MM-dd
        public DateOnly Date { get; set; }

        public string Description { get; set; }
    }
}
