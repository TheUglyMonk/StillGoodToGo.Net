
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Dtos;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Mappers;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;

namespace StillGoodToGo.Controllers
{
    /// <summary>
    /// controller for the establishment entity.
    /// </summary>

    [ApiController]
    [Route("api/[controller]")]
    public class EstablishmentController : ControllerBase
    {

        private readonly IEstablishmentService _establishmentService;
        private readonly EstablishmentMapper _establishmentMapper;
        private readonly ILogger<EstablishmentController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EstablishmentController"/> class.
        /// </summary>
        /// <param name="establishmentService"></param>
        public EstablishmentController(IEstablishmentService establishmentService,
                                   EstablishmentMapper establishmentMapper,
                                   ILogger<EstablishmentController> logger)
        {
            _establishmentService = establishmentService;
            _establishmentMapper = establishmentMapper;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new establishment to the database.
        /// </summary>
        /// <param name="establishmentRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEstablishment([FromBody] EstablishmentRequestDto establishmentRequestDto)
        {
            try
            {
                _logger.LogInformation("Received EstablishmentRequestDto: {@EstablishmentRequestDto}", establishmentRequestDto);
                // Map the request DTO to an establishment model
                var establishment = _establishmentMapper.EstablishmentRequestToEstablishment(establishmentRequestDto);

                // Add the establishment to the database
                var addedEstablishment = await _establishmentService.AddEstablishment(establishment);

                // Map the added establishment to a response DTO and return it
                var establishmentResponseDto = _establishmentMapper.EstablishmentToEstablishmentResponse(addedEstablishment);

                return Ok(establishmentResponseDto);
            }
            catch (DbSetNotInitialize ex)
            {
                // Indicates a configuration error or similar internal problem.
                return StatusCode(500, ex.Message);
            }
            catch (ParamIsNull ex)
            {
                // Indicates that a required parameter was null.
                return BadRequest(ex.Message);
            }
            catch (EstablishmentNotUnique ex)
            {
                // Indicates a conflict when the establishment email or location is not unique.
                return Conflict(ex.Message);
            }
            catch (NoCategoryFound ex)
            {
                // Indicates that no category was provided.
                return BadRequest(ex.Message);
            }
            catch (InvalidCategoryFound ex)
            {
                // Indicates that one or more categories are invalid.
                return BadRequest(ex.Message);
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

