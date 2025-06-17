using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Turnero.Data.Context
{
    // Esta clase le dice a EF Core
    // cómo construir el DbContext en tiempo de diseño
    public class TurneroDbContextFactory : IDesignTimeDbContextFactory<TurneroDbContext>
    {
        public TurneroDbContext CreateDbContext(string[] args)
        {
            // 1) Construimos las opciones
            var optionsBuilder = new DbContextOptionsBuilder<TurneroDbContext>();

            // 2) Usamos la misma cadena de conexión que en appsettings.json
            //    Ajusta aquí usuario, contraseña y versión de MySQL según tu setup local
            var connectionString = "server=localhost;port=3306;database=TurneroLab;user=turnero_user;password=Turn3r0P4ss!;SslMode=Preferred";

            // 3) Configuramos el proveedor MySQL (Pomelo) y la versión del servidor
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 32))
            );

            // 4) Devolvemos el DbContext ya construido
            return new TurneroDbContext(optionsBuilder.Options);
        }
    }
}
