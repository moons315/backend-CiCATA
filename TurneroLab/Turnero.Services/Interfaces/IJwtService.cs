namespace Turnero.Services.Interfaces
{
    using Turnero.Domain.Entities;

    /// <summary>
    /// Servicio para generar JWTs.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Genera un token JWT para el usuario dado.
        /// </summary>
        string GenerateToken(User user);
    }
}
