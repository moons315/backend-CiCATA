using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Turnero.Infrastructure.Email;

namespace Turnero.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmailController : ControllerBase
    {
        // GET api/v1/email/test
        [HttpGet("test")]
        public async Task<IActionResult> TestEmail([FromServices] IEmailService email)
        {
            await email.SendEmailAsync(
                "nico.segov@gmail.com",
                "Prueba de correo",
                "<p>MailKit funciona correctamente!</p>"
            );
            return Ok("Correo enviado");
        }
    }
}
