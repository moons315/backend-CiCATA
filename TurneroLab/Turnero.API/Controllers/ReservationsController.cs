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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationsController(IReservationService service)
        {
            _service = service;
        }

        // GET: api/v1/reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            var dtos = list.Select(r => ToDto(r));
            return Ok(dtos);
        }

        // GET: api/v1/reservations/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id)
        {
            var r = await _service.GetByIdAsync(id);
            if (r is null) return NotFound();
            return Ok(ToDto(r));
        }

        // POST: api/v1/reservations
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> Create([FromBody] CreateReservationDto dto)
        {
            var entity = new Reservation
            {
                UserId = dto.UserId,
                DeviceId = dto.DeviceId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Observations = dto.Observations ?? ""
            };
            var created = await _service.CreateAsync(entity);
            var result = ToDto(created);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/v1/reservations/5/extend
        [HttpPut("{id:int}/extend")]
        public async Task<ActionResult<ReservationDto>> Extend(int id, [FromBody] ExtendReservationDto dto)
        {
            var updated = await _service.ExtendAsync(id, dto.NewEndTime, dto.Observations);
            return Ok(ToDto(updated));
        }

        // PUT: api/v1/reservations/5/release
        [HttpPut("{id:int}/release")]
        public async Task<ActionResult<ReservationDto>> Release(int id)
        {
            var updated = await _service.ReleaseAsync(id);
            return Ok(ToDto(updated));
        }

        // PUT: api/v1/reservations/5/complete
        [HttpPut("{id:int}/complete")]
        public async Task<ActionResult<ReservationDto>> Complete(int id)
        {
            var updated = await _service.CompleteAsync(id);
            return Ok(ToDto(updated));
        }

        // DELETE: api/v1/reservations/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _service.CancelAsync(id);
            return NoContent();
        }

        // Helper to map entity → DTO
        private static ReservationDto ToDto(Reservation r) => new()
        {
            Id = r.Id,
            UserId = r.UserId,
            Username = r.User?.Username,
            DeviceId = r.DeviceId,
            DeviceName = r.Device?.Name,
            LabId = r.Device?.LabId ?? 0,
            LabName = r.Device?.Lab?.Name,
            ProcessId = r.Device?.ProcessId ?? 0,
            ProcessName = r.Device?.Process?.Name,
            StartTime = r.StartTime,
            EndTime = r.EndTime,
            Status = r.Status,
            Observations = r.Observations,
            CreatedAt = r.CreatedAt,
            Extensions = r.Extensions.Select(e => new ReservationExtensionDto
            {
                Id = e.Id,
                RequestedAt = e.RequestedAt,
                NewEndTime = e.NewEndTime,
                Approved = e.Approved,
                Observations = e.Observations
            })
        };
    }
}
