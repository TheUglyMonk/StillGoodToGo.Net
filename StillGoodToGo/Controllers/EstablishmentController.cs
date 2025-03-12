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
                var establishment = _establishmentMapper.EstablishmentRequestToEstablishment(establishmentRequestDto);

                var addedEstablishment = await _establishmentService.AddEstablishment(establishment);

                var establishmentResponseDto = _establishmentMapper.EstablishmentToEstablishmentResponse(addedEstablishment);

                return Ok(establishmentResponseDto);
            }
            catch (DbSetNotInitialize ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (ParamIsNull ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EstablishmentNotUnique ex)
            {
                return Conflict(ex.Message);
            }
            catch (NoCategoryFound ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidCategoryFound ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing establishment in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the establishment to be updated.</param>
        /// <param name="establishmentDto">The DTO containing updated establishment details.</param>
        /// <returns>Returns an IActionResult with the response of the operation.</returns>
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
                var deactivatedEstablishment = await _establishmentService.DeactivateEstablishment(id);

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
                Establishment establishment = await _establishmentService.GetEstablishmentById(id);

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
                var establishments = await _establishmentService.GetEstablishmentsByDescription(description);

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
                var establishments = await _establishmentService.GetEstablishments();

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

        /// <summary>
        /// Gets all active Establishments
        /// </summary>
        /// <returns>List of active establishments</returns>
        // GET api/establishment/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveEstablishments()
        {
            try
            {
                var establishments = await _establishmentService.GetActiveEstablishments();

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

        /// <summary>
        /// Adds a profit amount to the total received for an establishment.
        /// </summary>
        /// <param name="id">The ID of the establishment.</param>
        /// <param name="profit">The profit amount to be added.</param>
        /// <returns>The updated establishment details.</returns>
        /// <response code="200">Returns the updated establishment details.</response>
        /// <response code="404">If the establishment is not found.</response>
        /// <response code="500">If an internal server error occurs.</response>
        // POST api/establishment/addProfit/{id}
        [HttpPost("addProfit/{id}")]
        public async Task<IActionResult> AddsAmountReceived(int id, [FromBody] ProfitRequestDto profit)
        {
            try
            {
                Establishment establishment = await _establishmentService.AddsAmountReceived(id, profit.Value);

                var establishmentDto = _establishmentMapper.EstablishmentToEstablishmentResponse(establishment);

                return Ok(establishmentDto);
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


        /// <summary>
        /// Gets Establishments by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        // GET api/establishment/email
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetEstablishmentByEmail(string email)
        {
            try
            {
                var establishment = await _establishmentService.GetEstablishmentByEmail(email);

                if (establishment == null)
                {
                    return BadRequest();
                }

                var responseDto = _establishmentMapper.EstablishmentToEstablishmentRequest(establishment);

                return Ok(responseDto);
            }
            catch (NotFoundInDbSet ex)
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