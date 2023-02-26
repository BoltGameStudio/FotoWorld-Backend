using FotoWorldBackend.Models.OperatorModels;
using FotoWorldBackend.Services.Operator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FotoWorldBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {

        private readonly IOperatorService _operatorService;

        public OperatorController(IOperatorService operatorService)
        {
            _operatorService = operatorService;
        }

        [Route("create-offer")]
        [HttpPost]
        public IActionResult CreateOffer( [FromBody] IFormFile []files)
        {
            return Ok();
        }


    }
}
