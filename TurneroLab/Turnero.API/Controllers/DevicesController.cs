using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turnero.API.DTOs;
using Turnero.Domain.Entities;
using Turnero.Services.Interfaces;

namespace Turnero.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        // GET: api/v1/devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAll()
        {
            var devices = await _deviceService.GetAllAsync();
            var dtos = devices.Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                ProcessId = d.ProcessId,
                ProcessName = d.Process.Name,
                LabId = d.LabId,
                LabName = d.Lab.Name,
                DurationMinutes = d.DurationMinutes,
                ConfigJson = d.ConfigJson
            });
            return Ok(dtos);
        }

        // GET: api/v1/devices/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DeviceDto>> GetById(int id)
        {
            var d = await _deviceService.GetByIdAsync(id);
            if (d is null) return NotFound();
            var dto = new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                ProcessId = d.ProcessId,
                ProcessName = d.Process.Name,
                LabId = d.LabId,
                LabName = d.Lab.Name,
                DurationMinutes = d.DurationMinutes,
                ConfigJson = d.ConfigJson
            };
            return Ok(dto);
        }

        // GET: api/v1/devices/by-lab/{labId}
        [HttpGet("by-lab/{labId:int}")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetByLab(int labId)
        {
            var devices = await _deviceService.GetByLabIdAsync(labId);
            var dtos = devices.Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                ProcessId = d.ProcessId,
                ProcessName = d.Process.Name,
                LabId = d.LabId,
                LabName = d.Lab.Name,
                DurationMinutes = d.DurationMinutes,
                ConfigJson = d.ConfigJson
            });
            return Ok(dtos);
        }

        // POST: api/v1/devices
        [HttpPost]
        public async Task<ActionResult<DeviceDto>> Create([FromBody] CreateDeviceDto dto)
        {
            var device = new Device
            {
                Name = dto.Name,
                ProcessId = dto.ProcessId,
                LabId = dto.LabId,
                DurationMinutes = dto.DurationMinutes,
                ConfigJson = dto.ConfigJson ?? "{}"
            };
            var created = await _deviceService.CreateAsync(device);

            var resultDto = new DeviceDto
            {
                Id = created.Id,
                Name = created.Name,
                ProcessId = created.ProcessId,
                ProcessName = created.Process.Name,
                LabId = created.LabId,
                LabName = created.Lab.Name,
                DurationMinutes = created.DurationMinutes,
                ConfigJson = created.ConfigJson
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }
    }
}
