using FotoWorldBackend.Models.OperatorModels;
using FotoWorldBackend.Services.Operator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(Roles = "Operator")]
        public IActionResult CreateOffer([FromForm]CreateOfferModel offer)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims;
            var id = claims.FirstOrDefault(o => o.Type == "id").Value;
            var ret = _operatorService.CreateOffer(offer, id);
            if(ret != null)
            {
                return Ok(ret);
            }
            return BadRequest();
        }


        [Route("edit-offer/{id}")]
        [Consumes("multipart/form-data", "application/json")]
        [HttpPut]
        [Authorize(Roles = "Operator")]
        public IActionResult EditOffer([FromForm] CreateOfferModel offer, [FromRoute] int id)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims;
            var operatorId = claims.FirstOrDefault(o => o.Type == "id").Value;

            var ret = _operatorService.UpdateOffer(offer, operatorId, id);

            if(ret != null)
            {
                return Ok(ret);
            }
            return BadRequest();
        }
        

    }
}
