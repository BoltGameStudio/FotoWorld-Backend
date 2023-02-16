using FotoWorldBackend.Models;
using FotoWorldBackend.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FotoWorldBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public AuthController(IEmailService emailService)
        {
            _emailService = emailService;
        }



        [Route("sentTestMail")]
        [HttpPost]
        public IActionResult SendMail([FromBody] EmailModel reqest)
        {
            _emailService.SendEmail(reqest);
            return Ok();
        }
    }
}
