using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turnero.Services.Interfaces;
using Turnero.Services.DTOs;

namespace Turnero.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reports;

        public ReportsController(IReportService reports)
        {
            _reports = reports;
        }

        /// <summary>
        /// Informe de cancelaciones (byUser, byAdmin, rate)
        /// GET /api/v1/Reports/cancellations
        /// </summary>
        [HttpGet("cancellations")]
        public async Task<IActionResult> Cancellations()
        {
            var dto = await _reports.GetCancellationsAsync();
            return Ok(dto);
        }
    }
}
