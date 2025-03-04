using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Dtos;
using StillGoodToGo.Enums;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Services.ServicesInterfaces;


[ApiController]
[Route("api/publications")]
public class PublicationController : ControllerBase
{
    private readonly IPublicationService _publicationService;

    public PublicationController(IPublicationService publicationService)
    {
        _publicationService = publicationService ?? throw new ArgumentNullException(nameof(publicationService));
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
}
