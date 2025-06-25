namespace Turnero.API.DTOs
{
    /// <summary>
    /// DTO para la autenticación de usuarios.
    /// </summary>
    public class AuthenticateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
