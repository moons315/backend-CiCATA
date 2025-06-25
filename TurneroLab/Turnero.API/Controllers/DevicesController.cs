using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Turnero.Services.Interfaces;
using Turnero.API.DTOs;

namespace Turnero.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _deviceService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _deviceService.GetByIdAsync(id));

        [HttpGet("by-lab/{labId:int}")]
        public async Task<IActionResult> GetByLab(int labId)
            => Ok(await _deviceService.GetByLabIdAsync(labId));

        [HttpPost]
        public async Task<IActionResult> Create(CreateDeviceDto dto)
        {
            var dev = await _deviceService.CreateAsync(
                dto.Name,
                dto.LabId,
                dto.DurationMinutes,
                dto.ConfigJson
            );
            return CreatedAtAction(nameof(GetById), new { id = dev.Id }, dev);
        }
    }
}
