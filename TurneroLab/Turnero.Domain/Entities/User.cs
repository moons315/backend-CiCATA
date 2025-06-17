using System;
using System.Collections.Generic;

namespace Turnero.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Relación con Role
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; }

        // Un usuario puede tener muchas reservas
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
