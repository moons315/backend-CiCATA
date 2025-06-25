using Turnero.Services.DTOs;

 public interface IReportService
{
    Task<CancellationsReportDto> GetCancellationsAsync();
}
