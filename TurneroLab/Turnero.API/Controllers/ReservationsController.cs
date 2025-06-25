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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _resService;

        public ReservationsController(IReservationService resService)
        {
            _resService = resService;
        }

        // 1) Obtener todas las reservas
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _resService.GetAllAsync());

        // 2) Obtener una reserva por Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _resService.GetByIdAsync(id));

        // 3) Crear una nueva reserva
        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationDto dto)
        {
            var res = await _resService.CreateAsync(
                dto.UserId,
                dto.DeviceId,
                dto.StartTime,
                dto.EndTime,
                dto.Observations
            );
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        // 4) Cancelar propia reserva (por el usuario dueño)
        [HttpPut("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _resService.CancelAsync(id);
            return NoContent();
        }

        // 5) Cancelar reserva ajena (solo Administradores)
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id:int}/cancel-by-admin")]
        public async Task<IActionResult> CancelByAdmin(int id)
        {
            await _resService.CancelByAdminAsync(id);
            return NoContent();
        }

        // 6) Extender la reserva
        [HttpPut("{id:int}/extend")]
        public async Task<IActionResult> Extend(int id)
        {
            await _resService.ExtendAsync(id);
            return NoContent();
        }

        // 7) Liberar antes de tiempo
        [HttpPut("{id:int}/release")]
        public async Task<IActionResult> Release(int id)
        {
            await _resService.ReleaseAsync(id);
            return NoContent();
        }

        // 8) Marcar como completada
        [HttpPut("{id:int}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            await _resService.CompleteAsync(id);
            return NoContent();
        }
    }
}
