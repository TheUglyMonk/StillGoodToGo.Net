using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Dtos;
using StillGoodToGo.Enums;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Mappers;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;


namespace StillGoodToGo.Controllers
{
    [ApiController]
    [Route("api/publications")]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;
        private readonly PublicationMapper _publicationMapper;

        public PublicationController(IPublicationService publicationService, PublicationMapper publicationMapper)
        {
            _publicationService = publicationService ?? throw new ArgumentNullException(nameof(publicationService));
            _publicationMapper = publicationMapper;
        }

        [HttpPost]
        public async Task<ActionResult<PublicationResponseDto>> CreatePublication([FromBody] PublicationRequestDto publicationDto)
        {
            try
            {
                var createdPublication = await _publicationService.AddPublication(publicationDto);
                return CreatedAtAction(nameof(CreatePublication), new { id = createdPublication.Id }, createdPublication);
            }
            catch (EstablishmentNotFound)
            {
                return NotFound(new { message = "The specified establishment does not exist." });
            }
            catch (ParamIsNull)
            {
                return BadRequest(new { message = "The publication data is invalid." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetPublications(
                [FromQuery] Category? category,
                [FromQuery] double? latitude,
                [FromQuery] double? longitude,
                [FromQuery] double? maxDistance,
                [FromQuery] string? foodType,
                [FromQuery] double? minDiscount)
        {
            try
            {
                var publications = await _publicationService.GetFilteredPublications(
                    category, latitude, longitude, maxDistance, foodType, minDiscount);
                return Ok(publications);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get all publications.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPublications()
        {
            try
            {
                // Get all publications
                var publications = await _publicationService.GetAllPublications();

                // Map publications to response dtos
                var responseDtos = publications.Select(e => _publicationMapper.PublicationToPublicationResponse(e)).ToList();

                return Ok(responseDtos);
            }
            catch (DbSetNotInitialize ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { details = ex.Message });
            }

        }

        /// <summary>
        /// Get publication by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicationById(int id)
        {
            try
            {
                // Get publication by id
                var publication = await _publicationService.GetPublicationById(id);

                // Check if publication was found
                if (publication == null)
                {
                    return NotFound(new { message = "Publication not found." });
                }

                // Map publication to response dto
                var responseDto = _publicationMapper.PublicationToPublicationResponse(publication);

                return Ok(responseDto);
            }
            catch (ParamIsNull ex)
            {
                return BadRequest();
            }
            catch (DbSetNotInitialize ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }
    }
}