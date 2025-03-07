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
        /// <summary>
        /// Service for the establishment entity.
        /// </summary>
        private readonly IEstablishmentService _establishmentService;

        /// <summary>
        /// Mapper for the establishment entity.
        /// </summary>
        private readonly EstablishmentMapper _establishmentMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EstablishmentController"/> class.
        /// </summary>
        /// <param name="establishmentService"></param>
        public EstablishmentController(IEstablishmentService establishmentService,
                                   EstablishmentMapper establishmentMapper)
        {
            _establishmentService = establishmentService;
            _establishmentMapper = establishmentMapper;
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

        /// <summary>
        /// Updates an establishment in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="establishmentDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstablishment(int id, [FromBody] EstablishmentRequestDto establishmentDto)
        {
            try
            {
                // Map the request DTO to an establishment model
                Establishment establishment = _establishmentMapper.EstablishmentRequestToEstablishment(establishmentDto);

                // Call the service to update the establishment
                establishment = await _establishmentService.UpdatesEstablishment(id, establishment);

                // Map the updated establishment to a response DTO and return it
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

        /// <summary>
        /// Deactivates an establishment in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> DeactivateEstablishment(int id)
        {
            try
            {
                // Call the service to deactivate the establishment.
                var deactivatedEstablishment = await _establishmentService.DeactivateEstablishment(id);

                // Map the updated establishment to a response DTO.
                var responseDto = _establishmentMapper.EstablishmentToEstablishmentResponse(deactivatedEstablishment);

                return Ok(responseDto);
            }
            catch (DbSetNotInitialize ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (NotFoundInDbSet ex)
            {
                return NotFound(ex.Message);
            }
            catch (EstablishmentAlreadyDesactivated ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets an establishment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/establishment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstablishmentById(int id)
        {
            try
            {
                // Call the service to get the establishment by id.
                Establishment establishment = await _establishmentService.GetEstablishmentById(id);

                // Map the establishment to a response DTO.
                EstablishmentResponseDto responseDto = _establishmentMapper.EstablishmentToEstablishmentResponse(establishment);

                return Ok(responseDto);
            }
            catch (NotFoundInDbSet ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidParam ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Gets Establishments by description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        // GET api/establishment/description/{description}
        [HttpGet("description/{description}")]
        public async Task<IActionResult> GetEstablishmentsByDescription(string description)
        {
            try
            {
                // Call the service to get the establishments by description.
                var establishments = await _establishmentService.GetEstablishmentsByDescription(description);
                // Map the establishments to response DTOs.
                var responseDtos = establishments.Select(e => _establishmentMapper.EstablishmentToEstablishmentResponse(e)).ToList();

                return Ok(responseDtos);
            }
            catch (NotFoundInDbSet ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidParam ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Gets all Establishments
        /// </summary>
        /// <returns>List of establishments</returns>
        // GET api/establishment
        [HttpGet]
        public async Task<IActionResult> GetEstablishments()
        {
            try
            {
                // Call the service to get the establishments.
                var establishments = await _establishmentService.GetEstablishments();

                // Map the establishments to response DTOs.
                var responseDtos = establishments.Select(e => _establishmentMapper.EstablishmentToEstablishmentResponse(e)).ToList();

                return Ok(responseDtos);
            }

            catch (NotFoundInDbSet ex)
            {
                return NotFound(ex.Message);
            }
            catch (DbSetNotInitialize ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}