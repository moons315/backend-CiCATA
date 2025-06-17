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
    public class LabsController : ControllerBase
    {
        private readonly ILabService _labService;

        public LabsController(ILabService labService)
        {
            _labService = labService;
        }

        // GET: api/v1/labs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabDto>>> GetAll()
        {
            var labs = await _labService.GetAllAsync();
            var dtos = labs.Select(l => new LabDto
            {
                Id = l.Id,
                Name = l.Name,
                Location = l.Location,
                Description = l.Description
            });
            return Ok(dtos);
        }

        // GET: api/v1/labs/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LabDto>> GetById(int id)
        {
            var l = await _labService.GetByIdAsync(id);
            if (l is null) return NotFound();
            var dto = new LabDto
            {
                Id = l.Id,
                Name = l.Name,
                Location = l.Location,
                Description = l.Description
            };
            return Ok(dto);
        }

        // POST: api/v1/labs
        [HttpPost]
        public async Task<ActionResult<LabDto>> Create([FromBody] CreateLabDto dto)
        {
            var lab = new Lab
            {
                Name = dto.Name,
                Location = dto.Location ?? "",
                Description = dto.Description ?? ""
            };
            var created = await _labService.CreateAsync(lab);

            var resultDto = new LabDto
            {
                Id = created.Id,
                Name = created.Name,
                Location = created.Location,
                Description = created.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }
    }
}
