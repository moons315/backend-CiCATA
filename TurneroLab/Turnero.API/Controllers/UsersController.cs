using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turnero.API.DTOs;
using Turnero.Domain.Entities;
using Turnero.Services.Interfaces;

namespace Turnero.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/v1/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/v1/users/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null)
                return NotFound();
            return Ok(user);
        }

        // POST: api/v1/users
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] CreateUserDto dto)
        {
            // Mapea DTO a entidad
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            var created = await _userService.CreateAsync(user, dto.Password);

            // Devuelve 201 Created con la ruta al nuevo recurso
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // POST: api/v1/users/authenticate
        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate([FromBody] CreateUserDto dto)
        {
            var valid = await _userService.ValidateCredentialsAsync(dto.Username, dto.Password);
            if (!valid)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

            // Aquí podrías generar y devolver un JWT
            return Ok(new { message = "Credenciales válidas" });
        }
    }
}
