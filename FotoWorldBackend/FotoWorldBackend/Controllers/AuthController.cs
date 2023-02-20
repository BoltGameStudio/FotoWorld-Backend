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
        private SymmetricEncryption _encryption = new SymmetricEncryption();

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
        [Route("Register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserModel reqest)
        {
            var user= _authService.RegisterUser(reqest);
            _emailService.SendActivationEmailUser(user);
            return Ok();
        }


        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="reqest">Register Data</param>
        /// <returns></returns>
        [Route("RegisterOperator")]
        [HttpPost]
        public IActionResult RegisterOperator([FromBody] RegisterOperatorModel reqest)
        {
            var userOperator = _authService.RegisterOperator(reqest);


            RegisterUserModel userCredentials = new RegisterUserModel();
            userCredentials.PhoneNumber = reqest.PhoneNumber;
            userCredentials.Email= reqest.Email;
            userCredentials.Password= reqest.Password; 
            userCredentials.Username= reqest.Username;
            userCredentials.RepeatPassword= reqest.RepeatPassword;

            var user = _authService.RegisterUser(userCredentials);

            _emailService.SendActivationEmailOperator(user, userOperator);
            return Ok();
        }





        /// <summary>
        /// Activates user with given ID
        /// </summary>
        /// <param name="id">encrypted id passed in url</param>
        /// <returns></returns>
        [Route("activate-user/{id}")]
        [HttpPost]
        public IActionResult ActivateUser([FromRoute] string id)
        {
            

            var userIdDecrypted = _encryption.Decrypt(_config.GetSection("SECRET_KEY").Value, id);
            var activated = _authService.ActivateUser(Convert.ToInt32(userIdDecrypted));
            if (activated)
            {
                return Ok(userIdDecrypted);
            }
            return BadRequest(userIdDecrypted);
        }



        /// <summary>
        /// Activates operator (and created with it user) with given ID
        /// </summary>
        /// <param name="operatorId">encrypted operator id passed in url</param>
        /// <param name="userId">encrypted user id passed in url</param>
        /// <returns></returns>
        [Route("activate-operator/{operatorId}/{userId}")]
        [HttpPost]
        public IActionResult ActivateOperator([FromRoute] string operatorId, string userId)
        {
            var operatorIdDecrypted = _encryption.Decrypt(_config.GetSection("SECRET_KEY").Value, operatorId);
            var userIdDecrypted = _encryption.Decrypt(_config.GetSection("SECRET_KEY").Value, userId);

            var activatedOperator = _authService.ActivateOperator(Convert.ToInt32(operatorIdDecrypted));
            var activatedUser = _authService.ActivateUser(Convert.ToInt32(userIdDecrypted));

            if (activatedOperator && activatedUser)
            {
                return Ok(operatorIdDecrypted);
            }
            return BadRequest(operatorIdDecrypted);
        }







        //TEST

        [Route("Encrypt")]
        [HttpPost]
        public IActionResult Encrypt([FromBody] string text)
        {
           

            var ret = _encryption.Encrypt(_config.GetSection("SECRET_KEY").Value, text);

            return Ok(ret);
        }

        [Route("Decrypt")]
        [HttpPost]
        public IActionResult Decrypt([FromBody] string text)
        {
            

            var ret = _encryption.Decrypt(_config.GetSection("SECRET_KEY").Value, text);

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
