using FotoWorldBackend.Models.OperatorModels;
using FotoWorldBackend.Services.Operator;
using Microsoft.AspNetCore.Authorization;
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
        [Consumes("multipart/form-data", "application/json")]
        [HttpPost]
        [Authorize]
        public IActionResult CreateOffer([FromForm]CreateOfferModel offer)
        {

            _operatorService.UploadPhotos(offer);
            return Ok();
        }


    }
}
