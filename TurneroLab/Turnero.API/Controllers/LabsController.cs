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
    public class LabsController : ControllerBase
    {
        private readonly ILabService _labService;

        public LabsController(ILabService labService)
        {
            _labService = labService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _labService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _labService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateLabDto dto)
        {
            var lab = await _labService.CreateAsync(dto.Name, dto.Location);
            return CreatedAtAction(nameof(GetById), new { id = lab.Id }, lab);
        }
    }
}
