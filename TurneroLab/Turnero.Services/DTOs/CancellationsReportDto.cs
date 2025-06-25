namespace Turnero.Services.DTOs
{
    /// <summary>
    /// Número de cancelaciones por usuarios (propias),
    /// por administradores (ajenas) y tasa global de cancelación.
    /// </summary>
    public record CancellationsReportDto(
        int ByUserCount,
        int ByAdminCount,
        double CancellationRate
    );
}
