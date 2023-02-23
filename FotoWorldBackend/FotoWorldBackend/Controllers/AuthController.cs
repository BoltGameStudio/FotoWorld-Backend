using FotoWorldBackend.Models;
using FotoWorldBackend.Services.Auth;
using FotoWorldBackend.Services.Email;
using FotoWorldBackend.Services.Token;
using FotoWorldBackend.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace FotoWorldBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public AuthController(IEmailService emailService, IAuthService authService, IConfiguration config, ITokenService tokenService)
        {
            _emailService = emailService;
            _authService = authService;
            _config = config;
            _tokenService= tokenService;
        }


        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="reqestUser">Register Data</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("register")]
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

        /// <summary>
        /// Log user in
        /// </summary>
        /// <param name="login">login data</param>
        /// <returns>token with respective claims</returns>
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user=_authService.LoginUser(login);
            if(user != null)
            {
                if (!user.IsOperator && login.LoginAsOperator)
                {
                    return Unauthorized();
                }
                return Ok(_tokenService.GenerateToken(user, login.LoginAsOperator));
            }
            return NotFound();
        }


        /// <summary>
        /// Used to reset password, asks about email and sends confirmation
        /// </summary>
        /// <param name="email">email of account</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("forgot-password")]
        [HttpPost]
        public IActionResult ForgotPassword([FromBody] string email)
        {
            var res = _authService.GetUserByMail(email);
            if(res != null)
            {
                _emailService.SendRestartPasswordEmail(res);
                return Ok();
            }
            return NotFound();
        }

        /// <summary>
        /// restarts passwords of user with passed id
        /// </summary>
        /// <param name="id">Taken from url</param>
        /// <param name="restart">Restart password form</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("restart-password/{id}")]
        [HttpPost]
        public IActionResult RestartPassword([FromRoute] string id, [FromBody] RestartPasswordModel restart)
        {

            if(restart.NewPassword == restart.RepeatNewPassword)
            {
                var userIdDecrypted = SymmetricEncryption.Decrypt(_config.GetSection("SECRET_KEY").Value, id);
                
                var ret= _authService.RestartPassword(Convert.ToInt32(userIdDecrypted), restart.NewPassword);
                if (ret)
                {
                    return Ok();
                }
                
                
            }
            return BadRequest();
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
    }
}
