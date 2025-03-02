using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Dtos;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Mappers;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;

namespace StillGoodToGo.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class EstablishmentController : ControllerBase
    {
        
        private readonly IEstablishmentService _establishmentService;
        private readonly EstablishmentMapper _establishmentMapper;

        public EstablishmentController(IEstablishmentService establishmentService, EstablishmentMapper establishmentMapper)
        {
            _establishmentService = establishmentService;
            _establishmentMapper = establishmentMapper;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstablishment(int id, [FromBody] EstablishmentRequestDto establishmentDto)
        {
            try
            {
                
                Establishment establishment = _establishmentMapper.EstablishmentRequestToEstablishment(establishmentDto);

                establishment = await _establishmentService.UpdatesEstablishment(id, establishment);

                return Ok(_establishmentMapper.EstablishmentToEstablishmentResponse(establishment));
            }
            catch (DbSetNotInitialize e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidParam e)
            {
                return BadRequest(e.Message);
            }
            catch (ParamIsNull e)
            {
                return BadRequest(e.Message);
            }
            catch (NotFoundInDbSet e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

