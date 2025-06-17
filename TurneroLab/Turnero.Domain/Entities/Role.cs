using System.Collections.Generic;

namespace Turnero.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Un rol tiene muchos usuarios
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
