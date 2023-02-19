using FotoWorldBackend.Models;
using FotoWorldBackend.Services.Auth;
using FotoWorldBackend.Services.Email;
using FotoWorldBackend.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FotoWorldBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthController(IEmailService emailService, IAuthService authService, IConfiguration config)
        {
            _emailService = emailService;
            _authService = authService;
            _config = config;
        }



        [Route("sentTestMail")]
        [HttpPost]
        public IActionResult SendMail([FromBody] EmailModel reqest)
        {
            _emailService.SendEmail(reqest);
            return Ok();
        }


        [Route("Register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserModel reqest)
        {
            var user= _authService.RegisterUser(reqest);
            _emailService.SendActivationEmail(user);
            return Ok();
        }


        [Route("Encrypt")]
        [HttpPost]
        public IActionResult Encrypt([FromBody] string text)
        {
            SymmetricEncryption test = new SymmetricEncryption();
            
            var ret = test.Encrypt(_config.GetSection("SECRET_KEY").Value, text);

            return Ok(ret);
        }

        [Route("activate-account/{id}")]
        [HttpPost]
        public IActionResult ActivateAccount([FromRoute] string id)
        {
            SymmetricEncryption test = new SymmetricEncryption();

            var ret = test.Decrypt(_config.GetSection("SECRET_KEY").Value, id);
            var activated = _authService.ActivateAccount(Convert.ToInt32(ret));
            if (activated)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }



        [Route("Decrypt")]
        [HttpPost]
        public IActionResult Decrypt([FromBody] string text)
        {
            SymmetricEncryption test = new SymmetricEncryption();

            var ret = test.Decrypt(_config.GetSection("SECRET_KEY").Value, text);

            return Ok(ret);
        }
    }
}
