using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Services.ServicesInterfaces;

namespace StillGoodToGo.Controllers
{
    public class Establishment : Controller
    {
        [ApiController]
        [Route("api/establishment")]
        public class EstablishmentController : ControllerBase
        {
            private readonly IEstablishmentService _establishmentService;
            private readonly EstablishmentMapper _establishmentMapper;
            public EstablishmentController(IEstablishmentService establishmentService)
            {
                _establishmentService = establishmentService;
            }
            [HttpPost]
            public async Task<IActionResult> AddEstablishment(Establishment establishment)
            {
                try
                {
                    var addedEstablishment = await _establishmentService.AddEstablishment(establishment);
                    return Ok(addedEstablishment);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
