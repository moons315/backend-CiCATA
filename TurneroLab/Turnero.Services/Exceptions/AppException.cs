// Turnero.Services/Exceptions/AppException.cs
using System;

namespace Turnero.Services.Exceptions
{
    /// <summary>
    /// Excepción de aplicación para errores de negocio (p.ej. validaciones).
    /// </summary>
    public class AppException : Exception
    {
        public AppException() : base() { }
        public AppException(string message) : base(message) { }
        public AppException(string message, Exception inner) : base(message, inner) { }
    }
}
