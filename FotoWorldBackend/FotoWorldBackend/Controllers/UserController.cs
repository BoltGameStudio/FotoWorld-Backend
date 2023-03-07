using FotoWorldBackend.Services.UserS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FotoWorldBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("get-offer-detailed/{id}")]
        [HttpGet]
        public IActionResult GetOfferDetailed([FromRoute] int id)
        {
            var ret = _userService.GetOfferDetailed(id);
            return Ok(ret);

        }

        [Route("get-image/{id}")]
        [HttpGet]
        public IActionResult GetImage(int id) { 
        
            return File(_userService.GetImageById(id), "image/*");
        }
    }
}
