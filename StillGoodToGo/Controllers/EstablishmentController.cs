using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Dtos;
using StillGoodToGo.Mappers;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;

namespace StillGoodToGo.Controllers
{
    /// <summary>
    /// controller for the establishment entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        private readonly IEstablishmentService _establishmentService;
        private readonly EstablishmentMapper _establishmentMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EstablishmentController"/> class.
        /// </summary>
        /// <param name="establishmentService"></param>
        public EstablishmentController(IEstablishmentService establishmentService)
        {
            _establishmentService = establishmentService;
        }

        /// <summary>
        /// Adds a new establishment to the database.
        /// </summary>
        /// <param name="establishmentRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEstablishment(EstablishmentRequestDto establishmentRequestDto)
        {
            try
            {
                // Map the request DTO to an establishment model
                var establishment = _establishmentMapper.EstablishmentRequestToEstablishment(establishmentRequestDto);

                // Add the establishment to the database
                var addedEstablishment = await _establishmentService.AddEstablishment(establishment);

                // Map the added establishment to a response DTO and return it
                var establishmentResponseDto = _establishmentMapper.EstablishmentToEstablishmentResponse(addedEstablishment);

                return Ok(establishmentResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

