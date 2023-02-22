using FotoWorldBackend.Models;
using FotoWorldBackend.Services.Auth;
using FotoWorldBackend.Services.Email;
using FotoWorldBackend.Utilities;
using Microsoft.AspNetCore.Authorization;
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
        private TokenUtils _tokenUtils;

        public AuthController(IEmailService emailService, IAuthService authService, IConfiguration config)
        {
            _emailService = emailService;
            _authService = authService;
            _config = config;
           
        }


        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="reqest">Register Data</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserModel reqestUser)
        {
            var user= _authService.RegisterUser(reqestUser);
            _emailService.SendActivationEmailUser(user);
            return Ok();
        }


        /// <summary>
        /// Activates user with given ID
        /// </summary>
        /// <param name="id">encrypted id passed in url</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("activate-user/{id}")]
        [HttpPost]
        public IActionResult ActivateUser([FromRoute] string id)
        {
            

            var userIdDecrypted = SymmetricEncryption.Decrypt(_config.GetSection("SECRET_KEY").Value, id);
            var activated = _authService.ActivateUser(Convert.ToInt32(userIdDecrypted));
            if (activated)
            {
                return Ok(userIdDecrypted);
            }
            return BadRequest(userIdDecrypted);
        }






        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user=_authService.LoginUser(login);
            if(user != null)
            {
                return Ok(_tokenUtils.GenerateToken(user));
            }
            return NotFound();
        }


        //TEST

        [Route("Encrypt")]
        [HttpPost]
        public IActionResult Encrypt([FromBody] string text)
        {
           

            var ret = SymmetricEncryption.Encrypt(_config.GetSection("SECRET_KEY").Value, text);

            return Ok(ret);
        }

        [Route("Decrypt")]
        [HttpPost]
        public IActionResult Decrypt([FromBody] string text)
        {
            

            var ret = SymmetricEncryption.Decrypt(_config.GetSection("SECRET_KEY").Value, text);

            return Ok(ret);
        }


        [Route("sentTestMail")]
        [HttpPost]
        public IActionResult SendMail([FromBody] EmailModel reqest)
        {
            _emailService.SendEmail(reqest);
            return Ok();
        }



        [Route("connectionString")]
        [HttpGet]
        public IActionResult ConnectionString()
        {
            return Ok(_config.GetSection("DatabaseConnectionStrings:DevDatabase").Value);
        }
    }
}
