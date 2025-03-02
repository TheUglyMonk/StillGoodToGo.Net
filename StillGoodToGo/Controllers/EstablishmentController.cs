using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Mappers;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;

namespace StillGoodToGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

