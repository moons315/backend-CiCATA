using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Turnero.Services.Interfaces;
using Turnero.Services.Exceptions;
using Turnero.API.DTOs;
using Turnero.Infrastructure.Email;


namespace Turnero.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UsersController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // ------------------ Público: Registro ------------------
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var user = await _userService.CreateAsync(dto.Username, dto.Email, dto.Password, dto.RoleId);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // ------------------ Público: Login ------------------
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateDto dto)
        {
            var user = await _userService.AuthenticateAsync(dto.Username, dto.Password);
            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        // ------------------ Protegido: Obtener todos los usuarios ------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _userService.GetAllAsync());

        // ------------------ Protegido: Obtener un usuario por Id ------------------
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _userService.GetByIdAsync(id));
    }
}
